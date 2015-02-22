using UnityEngine;
using System.Collections.Generic;
using System.Xml;

public enum EntityActionResult
{
	Success,		// Action executed succesfully
	Failure,		// Action failed to execute
	Repeat,			// Action should be repeated
	Pending,		// Action works over a duration and will return later (seems unnatural, keeping it just in case)
	Error			// An error occured while trying to execute the action
}

public abstract class EntityAction
{ // Generic action, subclasses should only be action categories, which should in turn be subclassed for actual actions
	protected string _ownerID;

	public MapEntity owner
	{
		get
		{
			return GameData.currentState.entities[_ownerID];
		}
	}

	protected EntityAction(string ownerID)
	{
		this._ownerID = ownerID;
	}

	public static EntityAction CreateFromXmlNode(XmlNode node, string ownerID)
	{
		string classString = node.Attributes["class"] != null ? node.Attributes["class"].Value : null;
		string positionString = node.Attributes["position"] != null ? node.Attributes["position"].Value : null;
		string targetID = node.Attributes["target"] != null ? node.Attributes["target"].Value : null;

		// Dirty way for now
		try
		{
			System.Type actualType = System.Type.GetType(classString);
			if (actualType.BaseType == typeof(EntityInteraction))
			{
				return (EntityAction)actualType.GetConstructor(new System.Type[] { typeof(string), typeof(string) }).Invoke(new object[] { ownerID, targetID });
			}
			else if (actualType.BaseType == typeof(EntityTargetedAction))
			{
				return (EntityAction)actualType.GetConstructor(new System.Type[] { typeof(string), typeof(MapPosition) }).Invoke(new object[] { ownerID, MapPosition.FromString(positionString) });
			}
			else if (actualType.BaseType == typeof(EntityStateChange))
			{
				return (EntityAction)actualType.GetConstructor(new System.Type[] { typeof(string) }).Invoke(new object[] { ownerID });
			}
		}
		catch (System.Exception e)
		{
			Debug.LogException(e);
		}

		return null;
	}

	protected abstract EntityActionResult _Run();

	public EntityActionResult Run()
	{
		try
		{
			return this._Run();
		}
		catch (ActionException e)
		{
			Debug.LogException(e);
			return EntityActionResult.Error;
		}
	}

	public XmlDocument ToXml()
	{
		XmlDocument doc = new XmlDocument();
		XmlElement node = doc.CreateElement("action");

		Dictionary<string, string> attributes = this.XmlActionAttibutes();

		if (attributes.ContainsKey("class"))
		{ // Security check
			attributes.Remove("class");
		}

		attributes.Add("class", this.GetType().ToString());

		foreach (KeyValuePair<string, string> attribute in attributes)
		{
			node.SetAttribute(attribute.Key, attribute.Value);
		}

		return doc;
	}

	public abstract Dictionary<string, string> XmlActionAttibutes();

	public XmlNode Serialize(XmlDocument doc)
	{
		var action = doc.CreateElement("action");
		var attributes = XmlActionAttibutes();

		var c = doc.CreateAttribute("class");
		c.Value = ToString();
		action.AppendChild(c);

		foreach (var attribute in attributes) {
			var a = doc.CreateAttribute(attribute.Key);
			a.Value = attribute.Value;
			action.AppendChild(a);
		}

		return action;
	}
}

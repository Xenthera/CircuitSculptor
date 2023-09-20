using Godot;
using System;

public partial class WorldMouse : Camera3D
{
	[Export] private Node3D target;

	private Vector3 rayStart, rayEnd;

	private RayCast3D _raycast;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Vector2 mousePos = GetViewport().GetMousePosition();
		ProjectRayOrigin(mousePos);
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);

		var spaceState = GetWorld3D().DirectSpaceState;
		var mousePos = GetViewport().GetMousePosition();
		rayStart = ProjectRayOrigin(mousePos);
		rayEnd = rayStart + ProjectRayNormal(mousePos) * 2000;
		PhysicsRayQueryParameters3D p = PhysicsRayQueryParameters3D.Create(rayStart, rayEnd);
		p.CollisionMask = Convert.ToUInt32("1", 2);

		var intersect = spaceState.IntersectRay(p);
		if (intersect.Count > 0)
		{
			Vector3 pos = (Vector3)intersect["position"];
			pos.Y += 2;
			target.Position = pos;
		}

	}
}

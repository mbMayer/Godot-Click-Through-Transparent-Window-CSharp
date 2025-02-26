extends StaticBody2D

@export_range(0, 0, 1, "or_greater") var padding : int = 0

func _ready():
	get_window().size_changed.connect(resize)
	resize()

func resize():
	var size := get_viewport_rect().size
	for n in get_children():
		remove_child(n)
		n.queue_free() 
	for bound in [
			[Vector2(1, 0), padding],
			[Vector2(-1, 0), -size.x + padding],
			[Vector2(0, 1), padding],
			[Vector2(0, -1), -size.y + padding],
			]:
		var shape := WorldBoundaryShape2D.new()
		shape.normal = bound[0]
		shape.distance = bound[1]
		var collision_shape := CollisionShape2D.new()
		collision_shape.shape = shape
		add_child(collision_shape)

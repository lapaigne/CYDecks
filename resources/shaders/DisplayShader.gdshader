shader_type canvas_item;

uniform vec4 origin:source_color;
uniform vec4 new:source_color;

void fragment() {
	vec4 current_pixel = texture(TEXTURE, UV);
	
	if (current_pixel == origin)
		COLOR = new;
	else
		COLOR = current_pixel;
}

//void light() {
	// Called for every pixel for every light affecting the CanvasItem.
	// Uncomment to replace the default light processing function with this one.
//}

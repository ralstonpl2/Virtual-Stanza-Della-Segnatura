readme

To add functionality to clicking on a highlighted figure:

1. find the figure

2. find its child with "Collider" in the name

3. in the inspector, in the FigureUI script component, find "Selection Events"

4. 	a) For an event that happens when the figure is clicked on: use the + button on "On Select(FigureUI)"
	b) For an event that happens when the player clicks anywhere else (like a de-select): use the + button on "On Deselect(FigureUI)"

5. Select the object you want to affect, then use the drop-down menu to select a function.




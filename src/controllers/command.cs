namespace Commands;

// Abstract type for handline actions
// Will probably just be keypresses for now, but may want more complex (e.g. mouse movement) later
public abstract class Command{}

// If for whatever reason we don't have an action to pass along, pass this
public class NullCommand : Command{}
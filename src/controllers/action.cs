// Abstract type for handline actions
// Will probably just be keypresses for now, but may want more complex (e.g. mouse movement) later
abstract class Action{}

// If for whatever reason we don't have an action to pass along, pass this
class NullAction : Action{}
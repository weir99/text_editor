namespace Updates;

// Views should just be referring to their document, so won't pass along any info yet
// but may want to in the future
public class InsertUpdate : Update{}
public class WholeUpdate : Update{} //Tells view to update everything, we'll just use this for now
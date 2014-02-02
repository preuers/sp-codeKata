import org.scalatest.enablers.Length

object AnagramGenerator {

  def generateFor(text: String): Seq[String] =
    if (text == "") Array.empty[String]
    else generateForNotEmpty(text)

  private def generateForNotEmpty(text: String): Seq[String] = {
    var current: Seq[Step] = Array(new Step("", text))
    // expand until only single character remains
    for (_ <- 0 until text.length() - 1)
      current = expand(current)
    return current.map(step => step.expanded + step.remaining)
  }

  private def expand(seq: Seq[Step]): Seq[Step] = seq.flatMap(expand _)

  private def expand(step: Step): Seq[Step] = {
    for {
      i <- 0 until step.remaining.length()
      // avoid expansion to redundant anagrams for equal characters
      if (!wasEqualOneAlreadySelected(i, step.remaining))
    } yield new Step(step.expanded + step.remaining(i), getSubStringExcluding(i, step.remaining))
  }

  private def getSubStringExcluding(index: Int, text: String): String = {
    var result = text.substring(0, index)
    if (index < text.length() - 1)
      result += text.substring(index + 1, text.length())
    return result
  }

  private def wasEqualOneAlreadySelected(index: Int, text: String): Boolean = {
    val charAtIndex = text(index)
    for (i <- 0 until index) {
      if (text(i) == charAtIndex)
        return true
    }
    return false
  }

  private class Step(val expanded: String, val remaining: String)
}

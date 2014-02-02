import org.scalatest.FunSuite
import org.junit.runner.RunWith
import org.scalatest.junit.JUnitRunner
import org.scalatest.Matchers

@RunWith(classOf[JUnitRunner])
class AnagramGeneratorSuite extends FunSuite with Matchers {
  test("generate for empty string") {
    AnagramGenerator.generateFor("") should equal(Array[String]())
  }
  
  test("generate for string with length of 1") {
	  AnagramGenerator.generateFor("A") should equal(Array("A"))
  }
  
  test("generate for string with unique characters") {
    AnagramGenerator.generateFor("ABC").sortBy(x => x) should equal(
        Array("ABC", "ACB", "BAC", "BCA", "CAB", "CBA").sortBy(x => x))
  }
  
  test("generate for string with repeating characters") {
    AnagramGenerator.generateFor("ABBA").sortBy(x => x) should equal(
        Array("ABBA", "ABAB", "AABB", "BABA", "BAAB", "BBAA").sortBy(x => x))
  }
}

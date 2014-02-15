import org.scalatest.FunSuite
import org.junit.runner.RunWith
import org.scalatest.junit.JUnitRunner
import org.scalatest.Matchers

@RunWith(classOf[JUnitRunner])
class BowlingScorerSuite extends FunSuite with Matchers {
  test("Normal throws add up.") {
    BowlingScorer.score(Seq(1, 1)) should equal(2)
  }
  
  test("Spare counts next ball twice.") {
    BowlingScorer.score(Seq(1, 9, 5, 1)) should equal(21)
  }
  
  test("Strike counts next 2 balls twice.") {
    BowlingScorer.score(Seq(10, 1, 2)) should equal(16)
  }
  
  test("Ten strikes on the first ball of all ten frames.") {
    BowlingScorer.score(Seq(10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10)) should equal(300)
  }
  
  test("Nine pins hit on first ball of all ten frames") {
    BowlingScorer.score(Seq(9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0, 9, 0)) should equal(90)
  }
  
  test("Five pins on the first ball of all ten frames. Second ball of each frame hits all five remaining") {
    BowlingScorer.score(Seq(5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5, 5)) should equal(150)
  }
}

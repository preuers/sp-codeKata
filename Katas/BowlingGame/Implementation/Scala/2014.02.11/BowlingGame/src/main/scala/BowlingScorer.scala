import scala.collection.mutable.ListBuffer
import scala.collection.mutable.ArrayBuffer

object BowlingScorer {
  def score(balls: Seq[Int]): Int = {
    var scoreState = new ScoreState
    for (pins <- balls)
      scoreState.addBall(pins)
    return scoreState.score
  }

  private class ScoreState() {
    private var frames: ArrayBuffer[Frame] = new ArrayBuffer
    private var outerFrameScorers: ArrayBuffer[OuterFrameScorer] = new ArrayBuffer

    def addBall(pins: Int) {
      outerFrameScorers.foreach(x => x.notifyAboutNewBall(pins))
      if(requiresNewFrame)
        frames.append(new Frame)
      if(!currentFrame.isInnerReady)
        currentFrame.addBall(pins).foreach(outerFrameScorers.append(_))
    }
    
    def requiresNewFrame = frames.length == 0 || frames.length < 10 && currentFrame.isInnerReady
    
    def currentFrame = frames.last

    def score: Int = frames.foldLeft(0)((acc, frame) => acc + frame.score)
  }

  private class Frame {
    private var firstBall: Option[Int] = None
    private var secondBall: Option[Int] = None
    private var outerScore: Option[OuterFrameScorer] = None

    def isInnerReady: Boolean = (firstBall.isDefined && firstBall.get >= 10) || secondBall.isDefined

    def addBall(pins: Int): Option[OuterFrameScorer] = {
      if (!firstBall.isDefined) {
        firstBall = Some(pins)
        if (pins >= 10) return createAndSetOuterScorer(2)
      } else if (!secondBall.isDefined) {
        secondBall = Some(pins)
        if (firstBall.get + pins >= 10) return createAndSetOuterScorer(1)
      }
      None
    }

    def createAndSetOuterScorer(openBalls: Int): Option[OuterFrameScorer] = {
      outerScore = Some(new OuterFrameScorer(openBalls))
      return outerScore
    }

    def score: Int = firstBall.getOrElse(0) + secondBall.getOrElse(0) + outerScore.map(x => x.score).getOrElse(0)
  }

  private class OuterFrameScorer(private var openBalls: Int) {
    private var pinCount: Int = 0

    def notifyAboutNewBall(pins: Int) {
      if (openBalls > 0) pinCount += pins
      openBalls = openBalls - 1
    }
    
    def score = pinCount
  }
}

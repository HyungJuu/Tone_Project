using System.Windows.Media;

namespace LoginApp.ViewModels.SnakeGame
{
    public class SnakeSegment
    {
        public int X { get; set; }
        public int Y { get; set; }
        public double Rotation { get; set; }
        public Brush SnakeColor { get; set; } = Brushes.Green; 
    }
}

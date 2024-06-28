namespace Helpers
{
    public static class ImageExtensions
    {
        public static void SetAlpha(this UnityEngine.UI.Image image, float alpha)
        {
            var color = image.color;
            color.a = alpha;
            image.color = color;
        }
    }
}
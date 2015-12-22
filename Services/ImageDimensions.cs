using System;

public struct ImageDimension
{
    private readonly string sizeName;
    private readonly int width;
    private readonly int height;

    public string SizeName { get { return sizeName; } }
    public int Width { get { return width; } }
    public int Height { get { return height; } }

    public ImageDimension(string sizeName, int width, int height)
    {
        this.sizeName = sizeName;
        this.width = width;
        this.height = height;
    }
}
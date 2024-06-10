using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace FakePhotoshop
{
    public partial class Form1 : Form
    {
        Bitmap image = (Bitmap)Image.FromFile("../../Imagini/Copac2.jpeg");
        Bitmap destination;

        public Form1()
        {
            InitializeComponent();
            destination = new Bitmap(image.Width, image.Height);
            pictureBox1.Image = image;
        }

        // Reset
        private void button2_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = image;
        }

        // Gray Scale
        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < image.Width; i++)
                for (int j = 0; j < image.Height; j++)
                {
                    Color source = image.GetPixel(i, j);
                    int media = MediaAritmetica(source);
                    Color dest = Color.FromArgb(media, media, media);
                    destination.SetPixel(i, j, dest);
                }
            pictureBox1.Image = destination;
        }

        // Contrast
        private void button3_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < image.Width; i++)
                for (int j = 0; j < image.Height; j++)
                {
                    Color source = image.GetPixel(i, j);
                    int media = MediaAritmetica(source);
                    Color dest;

                    if (media < 128) // jumatate din 255, valoarea maxima posibila
                        dest = ChangeColorBy(source, -20);
                    else
                        dest = ChangeColorBy(source, 20);

                    destination.SetPixel(i, j, dest);
                }
            pictureBox1.Image = destination;
        }

        // Complementary
        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < image.Width; i++)
                for (int j = 0; j < image.Height; j++)
                {
                    Color source = image.GetPixel(i, j);

                    int red = 255 - source.R;
                    int green = 255 - source.G;
                    int blue = 255 - source.B;
                    Color dest = Color.FromArgb(red, green, blue);

                    destination.SetPixel(i, j, dest);
                }
            pictureBox1.Image = destination;
        }

        // Blur
        private void button5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < image.Width; i++)
                for (int j = 0; j < image.Height; j++)
                {
                    List<Color> colors = new List<Color>();
                    for (int k = i - 3; k <= i + 3; k++)
                        for (int l = j - 3; l <= j + 3; l++)
                        {
                            if (k >= 0 && k < image.Width && l >= 0 && l < image.Height)
                                colors.Add(image.GetPixel(k, l));
                        }

                    Color dest = MediaAritmetica(colors);
                    destination.SetPixel(i, j, dest);
                }
            pictureBox1.Image = destination;
        }

        private int MediaAritmetica(Color color)
        {
            return (color.R + color.G + color.B) / 3;
        }

        private Color MediaAritmetica(List<Color> colors)
        {
            int red = 0, green = 0, blue = 0;
            for (int i = 0; i < colors.Count; i++)
            {
                red = red + colors[i].R;
                green = green + colors[i].G;
                blue = blue + colors[i].B;
            }
            red = red / colors.Count;
            green = green / colors.Count;
            blue = blue / colors.Count;
            return Color.FromArgb(red, green, blue);
        }

        private Color ChangeColorBy(Color source, int value)
        {
            int red = source.R + value;
            int green = source.G + value;
            int blue = source.B + value;

            if (red < 0) red = 0;
            if (green < 0) green = 0;
            if (blue < 0) blue = 0;

            if (red > 255) red = 255;
            if (green > 255) green = 255;
            if (blue > 255) blue = 255;

            return Color.FromArgb(red, green, blue);
        }
       //filtru albastru
        private void button6_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    Color source = image.GetPixel(i, j);
                    Color dest = Color.FromArgb(source.R, Math.Min(255, source.G + 50), Math.Min(255, source.B + 50));
                    destination.SetPixel(i, j, dest);
                }
            }
            pictureBox1.Image = destination;
        }
        //sepia
        private void button7_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < image.Width; i++)
    {
        for (int j = 0; j < image.Height; j++)
        {
            Color source = image.GetPixel(i, j);
            int r = (int)(source.R * 0.393 + source.G * 0.769 + source.B * 0.189);
            int g = (int)(source.R * 0.349 + source.G * 0.686 + source.B * 0.168);
            int b = (int)(source.R * 0.272 + source.G * 0.534 + source.B * 0.131);

            // Amplificăm efectul Sepia prin creșterea coeficienților
            r = Math.Min(255, Math.Max(0, r + 40));
            g = Math.Min(255, Math.Max(0, g + 20));
            b = Math.Min(255, Math.Max(0, b));

            destination.SetPixel(i, j, Color.FromArgb(r, g, b));
        }
    }
    pictureBox1.Image = destination;
        }
        
        //versiune pixelata
        private void button9_Click(object sender, EventArgs e)
        {
            int pixelSize = 5; // Dimensiunea pixelilor

            for (int i = 0; i < image.Width; i += pixelSize)
            {
                for (int j = 0; j < image.Height; j += pixelSize)
                {
                    // Calculăm culoarea medie a zonei de pixeli
                    int avgR = 0, avgG = 0, avgB = 0;
                    int count = 0;
                    for (int x = i; x < i + pixelSize && x < image.Width; x++)
                    {
                        for (int y = j; y < j + pixelSize && y < image.Height; y++)
                        {
                            Color pixel = image.GetPixel(x, y);
                            avgR += pixel.R;
                            avgG += pixel.G;
                            avgB += pixel.B;
                            count++;
                        }
                    }
                    avgR /= count;
                    avgG /= count;
                    avgB /= count;

                    // Setăm toți pixelii din zona curentă la culoarea medie calculată
                    for (int x = i; x < i + pixelSize && x < image.Width; x++)
                    {
                        for (int y = j; y < j + pixelSize && y < image.Height; y++)
                        {
                            destination.SetPixel(x, y, Color.FromArgb(avgR, avgG, avgB));
                        }
                    }
                }
            }
            pictureBox1.Image = destination;
        }
               
        //mozaic
        private void button10_Click(object sender, EventArgs e)
        {
            int tileSize = 10; // dimensiunea fiecărui pătrat din mozaic

            for (int i = 0; i < image.Width; i += tileSize)
            {
                for (int j = 0; j < image.Height; j += tileSize)
                {
                    // Calculăm culoarea medie pentru regiunea mozaicului
                    int avgRed = 0, avgGreen = 0, avgBlue = 0;
                    int pixelCount = 0;

                    for (int x = i; x < i + tileSize && x < image.Width; x++)
                    {
                        for (int y = j; y < j + tileSize && y < image.Height; y++)
                        {
                            Color pixelColor = image.GetPixel(x, y);
                            avgRed += pixelColor.R;
                            avgGreen += pixelColor.G;
                            avgBlue += pixelColor.B;
                            pixelCount++;
                        }
                    }

                    avgRed /= pixelCount;
                    avgGreen /= pixelCount;
                    avgBlue /= pixelCount;

                    Color avgColor = Color.FromArgb(avgRed, avgGreen, avgBlue);

                    // Umplem regiunea mozaicului cu culoarea medie
                    for (int x = i; x < i + tileSize && x < image.Width; x++)
                    {
                        for (int y = j; y < j + tileSize && y < image.Height; y++)
                        {
                            destination.SetPixel(x, y, avgColor);
                        }
                    }
                }
            }

            pictureBox1.Image = destination;
        }
        //aspect vechi
        private void button11_Click(object sender, EventArgs e)
        {
            Bitmap artisticImage = ApplyArtisticOverlay(image);
            pictureBox1.Image = artisticImage;
        }

        // Apply Artistic Overlay Filter
        private Bitmap ApplyArtisticOverlay(Bitmap original)
        {
            Bitmap artisticImage = new Bitmap(original.Width, original.Height);

            // Load texture image (ex: Old Paper Texture)
            Bitmap texture = new Bitmap("../../Imagini/hartie.jpeg");

            // Apply overlay effect
            for (int x = 0; x < original.Width; x++)
            {
                for (int y = 0; y < original.Height; y++)
                {
                    // Get pixel color from texture image
                    Color texturePixel = texture.GetPixel(x % texture.Width, y % texture.Height);

                    // Get pixel color from original image
                    Color originalPixel = original.GetPixel(x, y);

                    // Combine colors (e.g., by averaging)
                    int avgR = (texturePixel.R + originalPixel.R) / 2;
                    int avgG = (texturePixel.G + originalPixel.G) / 2;
                    int avgB = (texturePixel.B + originalPixel.B) / 2;

                    // Set pixel color to artistic image
                    artisticImage.SetPixel(x, y, Color.FromArgb(avgR, avgG, avgB));
                }
            }

            return artisticImage;
        }

        private void button12_Click(object sender, EventArgs e)
            {
            Random random = new Random();

            // Intensitatea zgomotului (poate fi ajustată)
            int noiseIntensity = 50;

            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    Color source = image.GetPixel(i, j);

                    // Generăm un zgomot aleatoriu pentru fiecare canal de culoare
                    int red = Math.Max(0, Math.Min(255, source.R + random.Next(-noiseIntensity, noiseIntensity)));
                    int green = Math.Max(0, Math.Min(255, source.G + random.Next(-noiseIntensity, noiseIntensity)));
                    int blue = Math.Max(0, Math.Min(255, source.B + random.Next(-noiseIntensity, noiseIntensity)));

                    Color dest = Color.FromArgb(red, green, blue);
                    destination.SetPixel(i, j, dest);
                }
            }
            pictureBox1.Image = destination;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            int fragmentation = 10; // intensitatea fragmentării 
            Bitmap newImage = new Bitmap(image.Width, image.Height);

            for (int i = 0; i < image.Width; i++)
            {
                for (int j = 0; j < image.Height; j++)
                {
                    // Calculăm o nouă poziție pentru pixel, aplicând o mică deviere
                    int offsetX = random.Next(-fragmentation, fragmentation + 1);
                    int offsetY = random.Next(-fragmentation, fragmentation + 1);

                    int newX = i + offsetX;
                    int newY = j + offsetY;

                    // Asigurăm că noua poziție este în cadrul imaginii
                    newX = Math.Max(0, Math.Min(image.Width - 1, newX));
                    newY = Math.Max(0, Math.Min(image.Height - 1, newY));

                    Color source = image.GetPixel(newX, newY);
                    newImage.SetPixel(i, j, source);
                }
            }

            pictureBox1.Image = newImage;
        }
    }
}

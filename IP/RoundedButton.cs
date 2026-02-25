using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace IP
{
    public class RoundedButton : Button
    {
        private int _cornerRadius = 30;
        private Color _borderColor = Color.Transparent;
        private int _borderSize = 0;
        private Color _hoverBackColor;
        private Color _hoverBorderColor;
        private bool _isHovered = false;

        [Category("Rounded Button")]
        public int CornerRadius
        {
            get { return _cornerRadius; }
            set { _cornerRadius = Math.Max(0, value); Invalidate(); }
        }

        [Category("Rounded Button")]
        public int BorderSize
        {
            get { return _borderSize; }
            set { _borderSize = Math.Max(0, value); Invalidate(); }
        }

        [Category("Rounded Button")]
        public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; Invalidate(); }
        }

        [Category("Rounded Button")]
        public Color HoverBackColor { get; set; }

        [Category("Rounded Button")]
        public Color HoverBorderColor { get; set; }

        public RoundedButton()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.BackColor = Color.ForestGreen;
            this.ForeColor = Color.White;
            
            // Добавляем эффекты наведения
            this.MouseEnter += (s, e) => { _isHovered = true; Invalidate(); };
            this.MouseLeave += (s, e) => { _isHovered = false; Invalidate(); };
        }

        private GraphicsPath GetRoundPath(RectangleF Rect, int radius)
        {
            // Убедимся, что радиус не превышает размеры кнопки
            radius = Math.Min(radius, (int)Math.Min(Rect.Width / 2, Rect.Height / 2));
            
            GraphicsPath GraphPath = new GraphicsPath();
            GraphPath.AddArc(Rect.X, Rect.Y, radius, radius, 180, 90);
            GraphPath.AddArc(Rect.X + Rect.Width - radius, Rect.Y, radius, radius, 270, 90);
            GraphPath.AddArc(Rect.X + Rect.Width - radius, Rect.Y + Rect.Height - radius, radius, radius, 0, 90);
            GraphPath.AddArc(Rect.X, Rect.Y + Rect.Height - radius, radius, radius, 90, 90);
            GraphPath.CloseFigure();
            return GraphPath;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            
            RectangleF Rect = new RectangleF(0, 0, this.Width, this.Height);
            
            using (GraphicsPath GraphPath = GetRoundPath(Rect, _cornerRadius))
            {
                this.Region = new Region(GraphPath);
                
                // Определяем цвета в зависимости от состояния
                Color currentBackColor = _isHovered && HoverBackColor != Color.Empty 
                    ? HoverBackColor : this.BackColor;
                Color currentBorderColor = _isHovered && HoverBorderColor != Color.Empty 
                    ? HoverBorderColor : this.BorderColor;
                
                // Рисуем фон
                using (SolidBrush brush = new SolidBrush(currentBackColor))
                {
                    e.Graphics.FillPath(brush, GraphPath);
                }
                
                // Рисуем границу
                if (_borderSize > 0 && currentBorderColor != Color.Transparent)
                {
                    using (Pen pen = new Pen(currentBorderColor, _borderSize))
                    {
                        pen.Alignment = PenAlignment.Inset;
                        e.Graphics.DrawPath(pen, GraphPath);
                    }
                }
                
                // Рисуем текст или изображение
                if (this.Text != "")
                {
                    TextRenderer.DrawText(e.Graphics, this.Text, this.Font, 
                        this.ClientRectangle, this.ForeColor, 
                        TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | 
                        TextFormatFlags.WordBreak);
                }
                else if (this.BackgroundImage != null)
                {
                    // Если есть фоновое изображение (для кнопки backspace)
                    e.Graphics.DrawImage(this.BackgroundImage, 
                        new Rectangle((this.Width - this.BackgroundImage.Width) / 2,
                                     (this.Height - this.BackgroundImage.Height) / 2,
                                     this.BackgroundImage.Width, 
                                     this.BackgroundImage.Height));
                }
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }
    }
}
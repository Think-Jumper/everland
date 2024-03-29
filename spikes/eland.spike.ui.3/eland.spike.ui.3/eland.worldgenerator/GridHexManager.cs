﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using eland.worldgenerator.sl;
using unitstest.Interfaces;

namespace unitstest
{
    public class GridHexManager<T, TK> : IGridManager where T : IGridShapeFactory<TK> where TK : IGridShape
    {
        private readonly T _factory;
        public double ElementsX;
        public double ElementsY;
        public List<IGridShape> Grid;

        public GridHexManager(T factory)
        {
            _factory = factory;
        }

        public List<IGridShape> GetNeighbours(IGridShape centreGridSquare)
        {
            var neighbours = new List<IGridShape>();
            var currentColumn = centreGridSquare.Column;
            var currentRow = centreGridSquare.Row;

            if ((currentColumn + 2) % 2 == 0)
            {
                neighbours.Add(Grid.Where(g => (g.Row == currentRow) && (g.Column == currentColumn - 1)).SingleOrDefault());
                neighbours.Add(Grid.Where(g => (g.Row == currentRow - 1) && (g.Column == currentColumn)).SingleOrDefault());
                neighbours.Add(Grid.Where(g => (g.Row == currentRow) && (g.Column == currentColumn + 1)).SingleOrDefault());
                neighbours.Add(Grid.Where(g => (g.Row == currentRow+1) && (g.Column == currentColumn - 1)).SingleOrDefault());
                neighbours.Add(Grid.Where(g => (g.Row == currentRow+1) && (g.Column == currentColumn )).SingleOrDefault());
                neighbours.Add(Grid.Where(g => (g.Row == currentRow + 1) && (g.Column == currentColumn +1)).SingleOrDefault());
            }
            else{
                neighbours.Add(Grid.Where(g => (g.Row == currentRow - 1) && (g.Column == currentColumn - 1)).SingleOrDefault());
                neighbours.Add(Grid.Where(g => (g.Row == currentRow - 1) && (g.Column == currentColumn)).SingleOrDefault());
                neighbours.Add(Grid.Where(g => (g.Row == currentRow - 1) && (g.Column == currentColumn + 1)).SingleOrDefault());
                neighbours.Add(Grid.Where(g => (g.Row == currentRow) && (g.Column == currentColumn - 1)).SingleOrDefault());
                neighbours.Add(Grid.Where(g => (g.Row == currentRow) && (g.Column == currentColumn + 1)).SingleOrDefault());
                neighbours.Add(Grid.Where(g => (g.Row == currentRow + 1) && (g.Column == currentColumn)).SingleOrDefault());
            }

            return neighbours;

        }

        private static int GetHeight(IList<double> noise, int x, int y, int width)
        {
            return (int)noise[y*width + x];
        }

        public void Draw(Canvas surface, int gridSize, double strokeWidth, bool randomiseBlockedAreas, IList<double> noise)
        {
            ElementsX = Math.Floor(surface.ActualWidth / (gridSize * 2));
            ElementsY = Math.Floor(surface.ActualHeight / gridSize) * 2;
            
            double column = 0;
            double row = 0;

            surface.Children.Clear();
            Grid = new List<IGridShape>();

            var id = 0;

            for (var y = 0; y < ElementsY; y++)
            {
                row = (row + 2) % 2 == 0 ? 1 : 0;

                for (var x = 0; x < ElementsX; x++)
                {
                    var xx = ((x + 1) * (gridSize * 2));
                    if ((y+2)%2 != 0 )
                    {
                        xx = ((x + 1) * (gridSize * 2)) - gridSize;
                    }

                    var yy = (int)((y*gridSize) * 0.5);
                    var g = _factory.Create(xx, yy, gridSize, id++, false, (int)Math.Floor(column), (int)row, GetHeight(noise, xx, yy, (int)surface.ActualWidth) );

                    var poly = new Polygon();
                    var points = new PointCollection();

                    foreach (var p in g.Points) {
                        points.Add(p);
                    }

                    poly.Points = points;
                    poly.Fill = new SolidColorBrush(GetHexColour(g));
                    surface.Children.Add(poly);

                    Grid.Add(g);
                    row += 2;
                }
               
                column += 0.5;
            }

        }

        private static Color GetColourByName(ColorName color)
        {
            uint value = color;

            return (Color.FromArgb(
                (byte)(value >> 24),
                (byte)(value >> 16),
                (byte)(value >> 8),
                (byte)value)
            );
        }

        private static Color GetHexColour(IGridShape hex)
        {

            if (hex.Height < 30)
                return GetColourByName(ColorName.DarkBlue);
            if (hex.Height < 70)
                return GetColourByName(ColorName.LightBlue);
            if (hex.Height < 100)
                return GetColourByName(ColorName.Yellow);
            if (hex.Height < 120)
                return GetColourByName(ColorName.Wheat);
            if (hex.Height < 170)
                return GetColourByName(ColorName.LightGreen);
            if (hex.Height < 200)
                return GetColourByName(ColorName.Green);
            if (hex.Height < 220)
                return GetColourByName(ColorName.DarkGreen);
            if (hex.Height < 240)
                return GetColourByName(ColorName.LightGray);
            return GetColourByName(ColorName.DarkGray);

        }

        public void Block(Canvas surface, int x, int y)
        {
            var selectedHex = Grid.Where(hex => hex.Intersects(new Point(x, y))).FirstOrDefault();
            if (selectedHex == null) return;
            selectedHex.Blocked = true;

            HighLight(surface, selectedHex, Colors.Blue);

        }

        private static void HighLight(Panel surface, IGridShape hex, Color colour)
        {
            if (hex == null) return;
            var poly = new Polygon();
            var points = new PointCollection();

            foreach (var p in hex.Points)
            {
                points.Add(p);
            }

            poly.Points = points;
            poly.Stroke = new SolidColorBrush(Colors.LightGray);
            poly.Fill = new SolidColorBrush(colour);
            poly.StrokeThickness = 1;
            surface.Children.Add(poly);

        }

        public IGridShape HighlightShape(Canvas surface, int x, int y)
        {
            var shape = Grid.Where(hex => hex.Intersects(new Point(x, y))).SingleOrDefault();
            if (shape == null) return null;
           
            HighLight(surface, shape, Colors.Red);
            return shape;
        }

        public void HighlightGridShape(Canvas surface, List<IGridShape> shapes)
        {
            foreach(var shape in shapes)
            {
                HighLight(surface, shape, Colors.Yellow);
            }
        }

    
    }
}

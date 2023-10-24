﻿using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Shapes;
using _3_Lines_Shadow_Calculator.LineLibrary;
using _3_Lines_Shadow_Calculator.Models;

namespace _3_Lines_Shadow_Calculator;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private readonly Random _random = new();

    private ObservableCollection<LineInfo> LineSegmentsInfo { get; }
    private ObservableCollection<LineInfo> LineShadowsInfo { get; }

    public MainWindow()
    {
        InitializeComponent();
        
        LineSegmentsInfo = new ObservableCollection<LineInfo>();
        ListBoxLines.ItemsSource = LineSegmentsInfo;
        
        LineShadowsInfo = new ObservableCollection<LineInfo>();
        ListBoxShadows.ItemsSource = LineShadowsInfo;
    }

    private void ButtonAddLine_OnClick(object sender, RoutedEventArgs e)
    {
        try
        {
            AddLineAndRenderCanvasElements();
        }
        catch (FormatException)
        {
            MessageBox.Show("Please enter valid numbers for the line coordinates.", "Invalid Input",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void AddLineAndRenderCanvasElements()
    {
        // Add a new line segment.
        var randomY = GenerateRandomY();
        AddLineToCanvas(randomY);
        StoreInfoAboutLine(randomY);
        // Render The abscissa and The ordinate; and Line segments back.
        RenderXyPlane();
        // Re-render shadows.
        RenderShadows();

        //ResetInput();
    }

    private int GenerateRandomY() =>
    _random.Next(5, Constants.CanvasCenterY - 5);

    private Models.Point GenerateStartPoint(int randomY) =>
        new(TextBoxLineStartX.Text, randomY.ToString());

    private Models.Point GenerateEndPoint(int randomY) =>
        new(TextBoxLineEndX.Text, randomY.ToString());

    private void AddLineToCanvas(int randomY)
    {
        var lineSegment = LineSegmentCreator.CreateLineSegment(GenerateStartPoint(randomY), GenerateEndPoint(randomY));
        Canvas.Children.Add(lineSegment);
    }

    private void StoreInfoAboutLine(int randomY)
    {
        var lineSegmentInfo = new LineInfo(GenerateStartPoint(randomY), GenerateEndPoint(randomY));
        LineSegmentsInfo.Add(lineSegmentInfo);
    }

    private void RenderXyPlane()
    {
        Canvas.Children.Clear();
        InitializeXyPlaneLines();
        RenderLines();
    }

    private void InitializeXyPlaneLines()
    {
        var abscissa = LineSegmentCreator.CreateAbscissa();
        var ordinate = LineSegmentCreator.CreateOrdinate();
        Canvas.Children.Add(abscissa);
        Canvas.Children.Add(ordinate);
    }
    
    private void RenderLines()
    {
        foreach (var line in LineSegmentsInfo)
        {
            var lineSegment = LineSegmentCreator.CreateLineSegment(line.From, line.To);
            Canvas.Children.Add(lineSegment);
        }
    }

    private void RenderShadows()
    {
        LineShadowsInfo.Clear();
        
        var (shadows, lengths) = ShadowCreator.Create(LineSegmentsInfo);
        for (var i = 0; i < shadows.Count; i++)
        {
            Canvas.Children.Add(shadows[i]);
            AddShadowInfo(shadows[i], lengths[i]);
        }
    }

    private void AddShadowInfo(Line shadowLine, double length)
    {
        var from = new Models.Point(shadowLine.X1, shadowLine.Y1);
        var to = new Models.Point(shadowLine.X2, shadowLine.Y2);
        var lineSegmentInfo = new LineInfo(from, to, length);
        LineShadowsInfo.Add(lineSegmentInfo);
    }

    private void ResetInput()
    {
        TextBoxLineStartX.Text = string.Empty;
        TextBoxLineStartY.Text = string.Empty;
        TextBoxLineEndX.Text = string.Empty;
        TextBoxLineEndY.Text = string.Empty;
    }
}
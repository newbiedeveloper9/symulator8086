using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;

namespace symulator8086
{
    public partial class MainWindow : Window
    {
        private readonly Dictionary<string, ushort> MainRegisters = new()
        {
            {"AX", 0 },
            {"BX", 0 },
            {"CX", 0 },
            {"DX", 0 },
        };

        public MainWindow()
        {
            InitializeComponent();

            foreach (var register in MainRegisters)
            {
                comboBox1.Items.Add(register.Key);
                comboBox2.Items.Add(register.Key);
            }

            RefreshUi();
        }

        private void RefreshUi()
        {
            var uiBoxes = new[] { TxtBox1, TxtBox2, TxtBox3, TxtBox4 };
            var MainRegistersAsList = MainRegisters.ToList();
            for (int i = 0; i < MainRegistersAsList.Count; i++)
            {
                uiBoxes[i].Text = MainRegistersAsList[i].Value.ToString("X4");
            }

            Grid.UpdateLayout();
        }

        private static Random rnd = new();
        private void Random_Click(object sender, RoutedEventArgs e)
        {
            foreach (var register in MainRegisters)
            {
                MainRegisters[register.Key] = (ushort)rnd.Next(ushort.MinValue, ushort.MaxValue);
            }

            RefreshUi();
        }

        private void Reset_Click(object sender, RoutedEventArgs e)
        {
            foreach (var register in MainRegisters)
            {
                MainRegisters[register.Key] = 0;
            }

            RefreshUi();
        }

        private void Move_Click(object sender, RoutedEventArgs e)
        {
            var regs = (RegisterSrc: comboBox1?.SelectedValue?.ToString(), RegisterDst: comboBox2?.SelectedValue?.ToString());
            if(regs.RegisterSrc == null || regs.RegisterDst == null) 
            {
                return;
            }

            MainRegisters[regs.RegisterDst] = MainRegisters[regs.RegisterSrc];
            RefreshUi();
        }
        private void Exchange_Click(object sender, RoutedEventArgs e)
        {
            var regs = (RegisterSrc: comboBox1?.SelectedValue?.ToString(), RegisterDst: comboBox2?.SelectedValue?.ToString());
            if (regs.RegisterSrc == null || regs.RegisterDst == null)
            {
                return;
            }

            var srcVal = MainRegisters[regs.RegisterSrc]; 
            var dstVal = MainRegisters[regs.RegisterDst];

            MainRegisters[regs.RegisterSrc] = dstVal;
            MainRegisters[regs.RegisterDst] = srcVal;

            RefreshUi();
        }
    }
}
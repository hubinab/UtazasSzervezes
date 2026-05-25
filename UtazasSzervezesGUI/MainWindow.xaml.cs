using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using Repo.models;

namespace UtazasSzervezesGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        VizsgaContext tarolo = new VizsgaContext();

        public MainWindow()
        {
            InitializeComponent();
            cb1.ItemsSource = tarolo.Megrendelos.OrderBy(x => x.Nev).ToList();
            cb1.DisplayMemberPath = "Nev";
            cb2.IsEnabled = false;
        }

        private void cb1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lb1.Items.Clear();
            var selMegrendelo = cb1.SelectedItem as Megrendelo;
            if (selMegrendelo != null)
            {
                cb2.IsEnabled = true;
                cb2.ItemsSource = tarolo.Utazas
                    .Include(x => x.Uticel)
                    .Where(x => x.MegrendeloId == selMegrendelo.MegrendeloId)
                    .Select(x => x.Uticel)
                    .Distinct()
                    .ToList();
                cb2.DisplayMemberPath = "Megnevezes";
            }
        }

        private void cb2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selUticel = cb2.SelectedItem as Uticel;
            var selMegrendelo = cb1.SelectedItem! as Megrendelo;

            if (selUticel != null)
            {
                var uticelok = tarolo.Utazas.Where(x => x.Uticel.Megnevezes == selUticel.Megnevezes && x.Megrendelo == selMegrendelo);
                foreach (var item in uticelok)
                {
                    lb1.Items.Add($"Dátum: {item.Datum.ToString("yyyy-MM-dd")} Utasok száma: {item.Utasszam}");
                }
            }
        }
    }
}
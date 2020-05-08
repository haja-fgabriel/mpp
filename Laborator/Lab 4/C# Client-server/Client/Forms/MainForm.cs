using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Client.Forms;
using Model;
using Services.Utils;
using Services;

namespace Client
{
    public partial class MainForm : Form
    {
        private ClientController controller;
        private LoginForm loginForm;

        private List<Event> events;
        private List<Child> foundChildren;
        private List<Event> probe1ComboBoxevents;
        private List<Event> probe2ComboBoxevents;
        private Event selectedEvent;

        private Event GetEvent(int id)
        {
            var x = from Event e in events
                    where e.ID == id
                    select e;
            return x.ToList()[0];
        }

        public MainForm(LoginForm loginForm, ClientController controller)
        {
            this.loginForm = loginForm;
            this.controller = controller;
            InitializeComponent();
            controller.updateEvent += ChildUpdate;
            this.Shown += MainForm_Shown;
        }

        private void UpdateTables(Child child)
        {
            List<Event> eventsCopy = new List<Event>(events);

            Event e = selectedEvent;
            events.Clear();
            eventsCopy.ForEach((ev) =>
            {
                if (ev.ID == child.IdEvent1 || ev.ID == child.IdEvent2)
                    ev.No++;
                events.Add(ev);
            });

            if (e.ID == child.IdEvent1 || e.ID == child.IdEvent2)
            {
                foundChildren.Add(child);
            }
            // TODO update code
        }

        private void RefreshTable()
        {
            try
            {
                events = controller.GetAllEvents();
                probe1ComboBoxevents = new List<Event>(events);
                probe2ComboBoxevents = new List<Event>(events);

                eventsDataGridView.DataSource = null;
                eventsDataGridView.DataSource = events;
                eventsComboBox.DataSource = null;
                eventsComboBox.DataSource = events;
                probe1ComboBox.DataSource = null;
                probe1ComboBox.DataSource = probe1ComboBoxevents;
                probe2ComboBox.DataSource = null;
                probe2ComboBox.DataSource = probe2ComboBoxevents;
            }
            catch (Error e)
            {
                MessageBox.Show(this, "Unexpected error: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mainPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Search(Event e)
        {
            foundChildren = controller.GetFilteredChildren(e.ID, e.AgeMin, e.AgeMax);
            childrenDataGridView.DataSource = null;
            childrenDataGridView.DataSource = foundChildren;
        }

        private void Logout()
        { 
            controller.Logout();
            loginForm.Show();
            Hide();
        }

        private void SaveChild()
        {
            Child c = new Child()
            {
                ID = NumberUtils.GenerateNumber(10000, Int32.MaxValue),
                Name = nameTextBox.Text,
                Age = Int32.Parse(ageTextBox.Text),
                IdEvent1 = ((Event)probe1ComboBox.SelectedItem).ID,
                IdEvent2 = ((Event)probe2ComboBox.SelectedItem).ID
            };
            controller.SaveChild(c);
        }

        private void logoutButton_Click(object sender, EventArgs e)
        {
            Logout();
        }

        private void MainForm_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Logout();
            controller.updateEvent -= ChildUpdate;

            /* Preventing the disposal of the form */
            e.Cancel = true;
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            RefreshTable();
        }

        private void ChildUpdate(object sender, AthleticsEventArgs c)
        {
            if (c.AthleticsEventType == AthleticsEvent.ChildSaved)
            {
                /* TODO replace message box with actual refresh */
                MessageBox.Show(null, "Child saved.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                UpdateTables((Child)c.Data);
            }
        }

        private void saveChildButton_Click(object sender, EventArgs e)
        {
            SaveChild();
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            Search((Event)eventsComboBox.SelectedItem);
        }

        private void eventsComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            selectedEvent = (Event)eventsComboBox.SelectedItem;
        }
    }
}

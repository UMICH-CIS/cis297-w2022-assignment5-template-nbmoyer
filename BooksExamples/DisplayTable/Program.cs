using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace DisplayTable
{
    static class Program
    {
        public partial class Contacts : Form
        {
            public Contacts()
            {
                InitializeComponent();
            }

            // Entity Framework DbContext
            private DisplayTable.AddressBookEntities dbcontext = null;


            // fill our addressBindingSource with all rows, ordered by name
            private void RefreshContacts()
            {
                // Dispose old DbContext, if any
                if (dbcontext != null)
                {
                    dbcontext.Dispose();
                }

                // create new DbContext so we can reorder records based on edits
                dbcontext = new DisplayTable.AddressBookEntities();

                // use LINQ to order the Addresses table contents 
                // by last name, then first name
                dbcontext.Addresses
                .OrderBy(entry => entry.LastName)
                .ThenBy(entry => entry.FirstName)
                .Load();




                // specify DataSource for addressBindingSource
                addressBindingSource.DataSource = dbcontext.Addresses.Local;
                addressBindingSource.MoveFirst(); // go to first result

                findTextBox.Clear(); // clear the Find TextBox 
            }
            // when the form loads, fill it with data from the database
            private void Contacts_Load(object sender, EventArgs e)
            {
                RefreshContacts();
            }
        }
    }
}

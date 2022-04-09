using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DisplayTable
{
    public partial class DisplayAuthorsTable : Form
    {
        public DisplayAuthorsTable()
        {
            InitializeComponent();
        }

        //Entity Framework DbContext
        private BooksExamples.BooksEntities dbcontext = new BooksExamples.BooksEntities();
        //load data from database into DataGridView
        private void DisplayAuthorsTable_Load(object sender, EventArgs e)
        {
            //load Authors table ordered by LastName then FirstName
            dbcontext.Authors
                .OrderBy(author => author.LastName)
                .ThenBy(author => author.FirstName)
                .Load();
            //specify datasource for authorBindingSource
            authorBindingSource.DataSource = dbcontext.Authors.Local;

        }
        private void authorBindingNavigator_RefreshItems(object sender, EventArgs e)
        {
            //load Authors table ordered by LastName then FirstName
            dbcontext.Authors
                .OrderBy(author => author.LastName)
                .ThenBy(author => author.FirstName)
                .Load();
            //specify datasource for authorBindingSource
            authorBindingSource.DataSource = dbcontext.Authors.Local;
        }

        
        private void authorBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            Validate();
            authorBindingSource.EndEdit();
            try
            {
                dbcontext.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException)
            {
                MessageBox.Show("FirstName and LastName must contain values", "Entity Validation Exception");
            }
        }

        private void lastNameSearchButton_Click(object sender, EventArgs e)
        {
            string lastName = lastNameSearchBox.Text;

            var lastNameQuery =
            from Authors in dbcontext.Authors
            where Authors.LastName.StartsWith(lastNameSearchBox.Text)
            orderby Authors.LastName, Authors.FirstName
            select Authors;

            //addressBindingSource.DataSource = lastNameQuery.ToList();
            //addressBindingSource.MoveFirst();

            bindingNavigatorAddNewItem.Enabled = false;
            bindingNavigatorDeleteItem.Enabled = false;

        }

        private void lastNameSearchBox_TextChanged(object sender, EventArgs e)
        {
            
        }

        
        private void browesAllButton_Click(object sender, EventArgs e)
        {
            lastNameSearchBox.Text = "";
        }
    }
}

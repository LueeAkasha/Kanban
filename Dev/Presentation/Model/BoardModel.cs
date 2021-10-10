using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Model
{
    public class BoardModel : NotifiableModelObject
    {
        private ObservableCollection<ColumnModel> columns;
        public ObservableCollection<ColumnModel> Columns { get { return columns; } set { columns = value; } }
        private string email;
        public string Email { get { return email; } set { email = value; } }

        /// <summary>
        /// board model constructor.
        /// </summary>
        /// <param name="email"> user's email</param>
        /// <param name="ColumnsNames"> names of all columns in the user's board</param>
        /// <param name="controller"> backend controller</param>
        public BoardModel(string email, IReadOnlyCollection<string> ColumnsNames, BackendController controller):base(controller) {
            this.Email = email;
            this.Columns = new ObservableCollection<ColumnModel>();
            foreach (string ColumnName in ColumnsNames) {
                this.Columns.Add(Controller.GetColumn(Email, ColumnName));
            }
        }

        
    }
}

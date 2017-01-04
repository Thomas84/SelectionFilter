using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LazyBIM
{
    public partial class FormSelectionCategories : Form
    {
        public static IList<string> cat;
        public static bool cancel_form = false;
        public FormSelectionCategories()
        {
            InitializeComponent();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
            cancel_form = true;
        }

        private void btn_Select_Click(object sender, EventArgs e)
        {
            cat = new List<string>();
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                if (checkedListBox1.GetItemChecked(i))
                {
                    string str = (string)checkedListBox1.Items[i];

                    //special conditions
                    if (str == "Model Lines/Curves")
                    {
                        cat.Add("ModelArc");
                        cat.Add("ModelEllipse");
                        cat.Add("ModelHermiteSpline");
                        cat.Add("ModelLine");
                        cat.Add("ModelNurbSpline");
                    }
                    else
                    {
                        cat.Add(str);
                    }
                }
            }

            for (int i = 0; i < checkedListBox2.Items.Count; i++)
            {
                if (checkedListBox2.GetItemChecked(i))
                {
                    string str = (string)checkedListBox2.Items[i];

                    //special conditions
                    if (str == "Detail Lines/Curves")
                    {
                        cat.Add("DetailArc");
                        cat.Add("DetailEllipse");
                        cat.Add("DetailLine");
                        cat.Add("DetailNurbSpline");
                    }
                    else
                    { 
                        cat.Add(str);
                    }
                }
            }
            Close();
        }

    }
}
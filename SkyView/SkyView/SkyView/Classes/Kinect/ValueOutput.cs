using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SkyView.Classes.Kinect
{
    public partial class ValueOutput : Form
    {
        private int _iCount = 0;

        public ValueOutput()
        {
            InitializeComponent();
        }

        public void addValue( string val )
        {
            _iCount++;
            listBox1.Items.Insert( 0, _iCount + val );

        }

        private void ValueOutput_Load( object sender, EventArgs e )
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using NHibernate.Cfg;
using NHibernate;
using NHibernate.Collection.Generic;

namespace PBD_KR2
{ 
    public class NHibernateHelper
    {
        private static ISessionFactory sessionFactory;
        private static ISessionFactory SessionFactory
        {
            get
            {
                if (sessionFactory == null)
                {
                    sessionFactory = new Configuration().Configure().BuildSessionFactory();
                }
                return sessionFactory;
            }
        }
        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }

}

namespace PBD_KR2
{
    public partial class Form1 : Form
    {
        ISession session = null;

        private void Form1_Load(object sender, EventArgs e)
        {
            session = NHibernateHelper.OpenSession();

            IList<Authors> authors = session
                .CreateCriteria(typeof(Authors))
                .List<Authors>();

            dataGridView1.DataSource = authors;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView2.DataSource = (dataGridView1.CurrentRow.DataBoundItem as Authors).Graphs.ToList();
        }

        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            dataGridView3.DataSource = (dataGridView2.CurrentRow.DataBoundItem as Graphs).Vertexes.ToList();
        }

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridView dataGridView2;
        private System.Windows.Forms.DataGridView dataGridView3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

namespace PBD_KR2
{
    public class Authors
    {
        public virtual int Gid { get; set; }
        public virtual string LastName { get; set; }
        public virtual string FirstName { get; set; }
        public virtual PersistentGenericSet<Graphs> Graphs { get; set; }
    }
}

namespace PBD_KR2
{
    public class Graphs
    {
        public virtual int Gid { get; set; }
        public virtual string GName { get; set; }
        public virtual Authors AuthorGid { get; set; }
        public virtual DateTime DateChanged { get; set; }
        public virtual PersistentGenericSet<Vertexes> Vertexes { get; set; }
    }
}

namespace PBD_KR2

{
    public class Vertexes
    {

        public virtual Graphs GraphGid { get; set; }
        public virtual float PointX { get; set; }
        public virtual float PointY { get; set; }
        public virtual string VertexName { get; set; }
    }
}
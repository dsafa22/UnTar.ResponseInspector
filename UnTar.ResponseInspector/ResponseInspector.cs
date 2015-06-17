using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Fiddler;

namespace UnTar.ResponseInspector
{

    public class ResponseInspector : Inspector2, IResponseInspector2
    {
        InspectorControl control;
        public override void AddToTab(TabPage o)
        {
            control = new InspectorControl();
            o.Text = "UnTar";
            o.Controls.Add(control);
            o.Controls[0].Dock = DockStyle.Fill;
        }

        public override int GetOrder()
        {
            return 100;
        }

        #region IBaseInspector2 Members

        public void Clear()
        {
            return;
        }

        public bool bDirty
        {
            get { return false; }
        }

        private bool m_ReadOnly;
        public bool bReadOnly
        {
            get
            {
                return m_ReadOnly;
            }
            set
            {
                m_ReadOnly = value;
            }
        }


        private byte[] m_body;
        public byte[] body
        {
            get
            {
                return m_body;
            }
            set
            {
                m_body = value;

                var session = FiddlerApplication.UI.GetFirstSelectedSession();
                if (session.GetResponseBodyAsString().Length > 0)
                {
                    control.DoInspect(session);
                }
            }
        }

        #endregion

        #region IResponseInspector2 Members

        public HTTPResponseHeaders headers
        {
            get
            {
                return null;
            }
            set
            {

            }
        }

        #endregion

    }

}

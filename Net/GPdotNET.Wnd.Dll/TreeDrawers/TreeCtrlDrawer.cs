//////////////////////////////////////////////////////////////////////////////////////////
// GPdotNET - Genetic Programming Tool                                                  //
// Copyright 2006-2017 Bahrudin Hrnjica                                                 //
//                                                                                      //
// This code is free software under the GNU Library General Public License (LGPL)       //
// See license section of  https://github.com/bhrnjica/gpdotnet/blob/master/license.md  //
//                                                                                      //
// Bahrudin Hrnjica                                                                     //
// bhrnjica@hotmail.com                                                                 //
// Bihac,Bosnia and Herzegovina                                                         //
// http://bhrnjica.wordpress.com                                                        //
//////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using GPdotNet.Core;
using GPdotNet.Lib;
using GPdotNet.Interfaces;

namespace GPdotNet.Wnd.GUI
{
    public delegate string NodeValue(int nValue, IParameters param = null, bool usbscript = false);
    public delegate Brush NodeBackground(bool tag);

    /// <summary>
    /// This Class implements Grapg tree drawing control
    /// </summary>
    public partial class TreeCtrlDrawer : Panel
    {
        #region CTor and Private fields

        TreeNodeDrawer m_RootNode;
        LayeredTreeDraw _ltd;
        NodeValue _funNodeValueCallback = null;
        NodeBackground _funNodeBackgroundCallback=null;
        IParameters parameters=null;
        StringFormat sf;
        Pen connPen;
        Pen nodePen;

        //fill color node
        SolidBrush nodeBrush;
        //fill color for selected node
        SolidBrush selectedNodeBrush;
        //backgroud color when export to png
        SolidBrush backgroundColor;

        Font font, tfont;
        //foreground function color
        SolidBrush frgFColor;
        //foreground terminal color
        SolidBrush frgTColor ;

        public TreeCtrlDrawer()
            : base()
        {

            this.BackColor = SystemColors.Window;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.AutoScroll = true;

            sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            connPen = new Pen(Color.Black, 1.3f);
            nodePen = new Pen(Color.Black, 2f);

            //control background
            backgroundColor = new SolidBrush(SystemColors.Control);

            //this color depends of background of the Result panel
            nodeBrush = backgroundColor;//new SolidBrush(Color.White/*.FromArgb(201, 217, 239)*/);
            selectedNodeBrush = new SolidBrush(Color.OrangeRed);

           

            //
            font = new Font(new FontFamily("Segoe UI"), 12f, FontStyle.Regular);
            tfont = new Font(new FontFamily("Times New Roman"), 12f, FontStyle.Italic);
            frgFColor = new SolidBrush(Color.Black);
            frgTColor = new SolidBrush(Color.Black);
        }

        #endregion

        #region Properties
        public List<TreeNodeDrawer> Nodes
        {
            get;
            private set;
        }

        public TreeNodeDrawer RootNode
        {
            get
            {
                return m_RootNode;
            }
        }
        #endregion

        #region Private Internal and Protected Methods
       
        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (Nodes != null)
                {
                    sf.Dispose();
                    connPen.Dispose();
                    nodePen.Dispose();
                    nodeBrush.Dispose();
                    selectedNodeBrush.Dispose();
                    backgroundColor.Dispose();
                    font.Dispose();
                    font.Dispose();
                    frgFColor.Dispose();
                    frgTColor.Dispose();

                    Nodes.Clear();
                    Nodes = null;
                    m_RootNode = null;
                }
            }

            base.Dispose(disposing);
        }

        /// <summary>
        /// On Paint
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {

            base.OnPaint(e);

            drawNode(e.Graphics);
            drawConnection(e.Graphics);

        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //empty implementation
            e.Graphics.FillRectangle(backgroundColor, e.ClipRectangle);

        }

        /// <summary>
        /// Delets selected node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        internal bool DeleteNode(TreeNodeDrawer node)
        {
            //it is posibble to delete leaf node only

            if (node.TreeChildren == null || node.TreeChildren.Count == 0)
            {
                //delete node from all parent which has node as a children
                foreach (var no in Nodes)
                    no.TreeChildren.Remove(node);

                //delete all connection to deleted node
                _ltd.Connections.RemoveAll(c => c.IgnChild == node);

                //delete node from the collection
                bool retVal = Nodes.Remove(node);

                //redraw the layout
                RefreshTree();

                return retVal;
            }
            return false;
        }

        /// <summary>
        /// Helper methods for adding several node in to hierarchy manner
        /// </summary>
        /// <param name="value"></param>
        /// <param name="numArity"></param>
        /// <param name="treeNodeDrawer"></param>
        internal void AddNodeWithChildren(object value, int numArity, TreeNodeDrawer treeNodeDrawer)
        {
            TreeNodeDrawer parentNode;
            if (treeNodeDrawer == null)
                parentNode = AddNode(value,false, treeNodeDrawer);
            else
            {
                parentNode = treeNodeDrawer;
                parentNode.Content = value;
            }

            for(int i=0; i<numArity; i++)
                AddNode("o", false, parentNode);
           
            RefreshTree();
        }

        /// <summary>
        /// on mouse click event (virtual method)
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            if (Nodes == null)
                return;


            //Drawing the Node
            foreach (var node in Nodes)
            {
                if (node.Rect.Contains(e.Location))
                    node.Selected = true;
                else
                    node.Selected = false;
            }

            Invalidate();
        }

        /// <summary>
        /// Drawing allnodes in three control
        /// </summary>
        /// <param name="gr"></param>
        private void drawNode(Graphics gr)
        {

            if (Nodes == null)
                return;

            foreach (var tn in Nodes)
            {

                Point ptLocation = new Point(0, 0);
                if (tn != null)
                {
                    ptLocation = new Point(this.Margin.Left + (int)_ltd.X(tn) + base.AutoScrollPosition.X,
                                           this.Margin.Top + (int)_ltd.Y(tn) + base.AutoScrollPosition.Y);
                }
                tn.Rect = new Rectangle(ptLocation, tn.NodeSize);
            }

            //Drawing the Node
            int counter = 1;
            foreach (var node in Nodes)
            {
                if(_funNodeBackgroundCallback!=null)
                    gr.FillRectangle(_funNodeBackgroundCallback(node.Tag), node.Rect);

                counter++;

                if (node.Selected)
                    gr.FillRectangle(selectedNodeBrush, node.Rect);
                else
                    gr.FillRectangle(nodeBrush, node.Rect);

                gr.DrawRectangle(nodePen, node.Rect);

                if (node.Content == null)
                    return;

                int val = (int)node.Content;

                if (_funNodeValueCallback != null /* && node.Content.IsNumber()*/)
                    gr.DrawString(_funNodeValueCallback(val,parameters, true), val >= 2000 ? font : tfont, val >= 2000 ? frgFColor : frgTColor, node.Rect, sf);
                else
                    gr.DrawString(node.Content.ToString(), val >= 2000 ? font : tfont, frgFColor, node.Rect, sf);
            }
        }

        /// <summary>
        /// Proces of drqaing connection
        /// </summary>
        /// <param name="dc"></param>
        private void drawConnection(Graphics dc)
        {
            if (Nodes == null || _ltd == null)
                return;

            if (_ltd.Connections != null)
            {


                Point ptLast = new Point(this.Margin.Left, this.Margin.Top);
                bool fHaveLastPoint = false;

                foreach (TreeConnection tcn in _ltd.Connections)
                {
                    fHaveLastPoint = false;
                    foreach (DPoint dpt in tcn.LstPt)
                    {
                        if (!fHaveLastPoint)
                        {
                            ptLast = PtFromDPoint(tcn.LstPt[0]);
                            fHaveLastPoint = true;
                            continue;
                        }
                        dc.DrawLine(connPen, PtFromDPoint(dpt), ptLast);
                        ptLast = PtFromDPoint(dpt);
                    }
                }
            }
        }

        /// <summary>
        /// Converts point
        /// </summary>
        /// <param name="dPoint"></param>
        /// <returns></returns>
        private Point PtFromDPoint(DPoint dPoint)
        {
            return new Point((int)dPoint.X + base.AutoScrollPosition.X + this.Margin.Left, (int)dPoint.Y + base.AutoScrollPosition.Y + this.Margin.Top);

        }

        /// <summary>
        /// Init comp
        /// </summary>
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // TreeCtrlDrawer
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Padding = new System.Windows.Forms.Padding(5);
            this.ResumeLayout(false);

        }

        /// <summary>
        /// Return selected node
        /// </summary>
        /// <returns></returns>
        public TreeNodeDrawer GetSelectedNode()
        {
            if (Nodes == null)
                return null;
            //Drawing the Node
            foreach (var node in Nodes)
                if (node.Selected)
                    return node;
            return null;
        }

        /// <summary>
        /// Converts treeDrawer in to GPNode 
        /// </summary>
        /// <returns> GPnode tree structure</returns>
        public Node ToGPNode(bool tag=false)
        {
            if (this.RootNode == null)
                return null;

            //Collection holds tree nodes
            Queue<Node> dataTree = new Queue<Node>();
            Queue<TreeNodeDrawer> ctrls = new Queue<TreeNodeDrawer>();

            //current node
            Node root = new Node();
            TreeNodeDrawer treeCtrl = null;

            ctrls.Enqueue(this.RootNode);
            dataTree.Enqueue(root);

            while (ctrls.Count > 0)
            {
                //get next node
                var node = dataTree.Dequeue();
                treeCtrl = ctrls.Dequeue();
                node.Value = (int)treeCtrl.Content;
                node.marked = treeCtrl.Selected? !tag: tag;

                if (treeCtrl.TreeChildren != null && treeCtrl.TreeChildren.Count > 0)
                {
                    node.Children = new Node[treeCtrl.TreeChildren.Count];
                    for (int i = 0; i < treeCtrl.TreeChildren.Count; i++)
                    {
                        node.Children[i] = new Node();
                        node.Children[i].Level = (short)(node.Level + 1);
                        dataTree.Enqueue(node.Children[i]);

                        ctrls.Enqueue((TreeNodeDrawer)treeCtrl.TreeChildren[i]);
                    }
                }
            }

            return root;

        }

        /// <summary>
        /// Return selected node index
        /// </summary>
        /// <returns></returns>
        public int GetSelectedNodeIndex()
        {
            if (Nodes == null)
                return 0;
            //Drawing the Node
            int counter = 0;
            foreach (var node in Nodes)
            {
                if (node.Selected)
                    return counter;
                counter++;
            }
            return 0;
        }
        public int GetSelectedIndex()
        {
            int index = 0;
            var stack = new Queue<TreeNodeDrawer>();
            stack.Enqueue(m_RootNode);
            while (stack.Count > 0)
            {
                var node = stack.Dequeue();

                if (node.Selected)
                    return index;
                index++;
                //enumerate children
                if (node.TreeChildren != null)
                {
                    for (int i = node.TreeChildren.Count - 1; i >= 0; i--)
                        stack.Enqueue(node.TreeChildren[i] as TreeNodeDrawer);
                }

            }

            return -1;
        }
        /// <summary>
        /// Check is tree is empty without root
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return m_RootNode == null;
        }

        /// <summary>
        /// Clear all nodes and connections from the control
        /// </summary>
        public void Clear()
        {
            if (Nodes == null)
                return;
            Nodes.Clear();
          
        }

        /// <summary>
        /// Methods fo add new node in to tree structure
        /// </summary>
        /// <param name="nodeValue"></param>
        /// <param name="tnParent"></param>
        /// <returns></returns>
        public TreeNodeDrawer AddNode(object nodeValue, bool tag, TreeNodeDrawer tnParent = null)
        {
            if (tnParent != null && m_RootNode == null)
                throw new Exception("The m_RootNode three is null.");

            TreeNodeDrawer tnNew = new TreeNodeDrawer();
            tnNew.Content = nodeValue;
            tnNew.Tag = tag;

            //in case of long function name increase the width
            var name = Globals.FunctionFromId((int)nodeValue);
            if (name.Length > 4)
                tnNew.NodeSize = new System.Drawing.Size(120, 30);
            //this is root if parent is null
            if (tnParent != null)
                tnParent.TreeChildren.Add(tnNew);
            else
                m_RootNode = tnNew;

            if (Nodes == null)
                Nodes = new List<TreeNodeDrawer>();

            Nodes.Add(tnNew);
            return tnNew;
        }

        /// <summary>
        /// Prepare for Tree node draw.
        /// </summary>
        /// <param name="gpRoot"></param>
        /// <param name="funNodeValue">delegate for callback for retrieve GPNode string representation</param>
        public void DrawTreeExpression(Node gpRoot,NodeValue funNodeValue, IParameters param = null, NodeBackground funNodeBackground=null)
        {
            Clear();
            parameters = param;
            //define callbacks for 
            _funNodeValueCallback = funNodeValue;
            _funNodeBackgroundCallback = funNodeBackground;

            //Collection holds tree nodes
            Queue<Node> dataTree = new Queue<Node>();
            Queue<TreeNodeDrawer> ctrls = new Queue<TreeNodeDrawer>();

            //current node
            Node node = null;
            TreeNodeDrawer treeCtrl = null;
            treeCtrl = AddNode(gpRoot.Value, gpRoot.marked,null);

            ctrls.Enqueue(treeCtrl);
            dataTree.Enqueue(gpRoot);

            while (dataTree.Count > 0)
            {
                //get next node
                node = dataTree.Dequeue();
                treeCtrl = ctrls.Dequeue();

                if (node.Children != null)
                    for (int i = 0; i < node.Children.Length; i++)
                    {
                        var tn = AddNode(node.Children[i].Value,node.Children[i].marked, treeCtrl);
                        dataTree.Enqueue(node.Children[i]);
                        ctrls.Enqueue(tn);
                    }
            }

            _ltd = new LayeredTreeDraw(m_RootNode, 17.5, 17.5, 17.5, VerticalJustification.top);
            _ltd.LayoutTree();

            //Auto enable scroll for new content size
            this.AutoScrollMinSize = new Size((int)_ltd.PxOverallWidth + this.Margin.Left + this.Margin.Right, (int)_ltd.PxOverallHeight + this.Margin.Top + this.Margin.Bottom);
            Invalidate();

        }

        /// <summary>
        /// Refresh tree control
        /// </summary>
        public void RefreshTree()
        {
            //Auto enable scroll for new content size
            _ltd = new LayeredTreeDraw(m_RootNode, 17.5, 17.5, 17.5, VerticalJustification.top);
            _ltd.LayoutTree();
            this.AutoScrollMinSize = new Size((int)_ltd.PxOverallWidth + this.Margin.Left + this.Margin.Right, (int)_ltd.PxOverallHeight + this.Margin.Top + this.Margin.Bottom);
            Invalidate();
        }

        /// <summary>
        /// Svae tree control in to image
        /// </summary>
        /// <param name="path"></param>
        public void SaveAsImage(string path = "C:\\a.png", IParameters param = null)
        {
            backgroundColor = new SolidBrush(Color.White);
            nodeBrush = backgroundColor;

            parameters = param;
            //set bitmap state for control
            this.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.AutoScroll = false;
            this.HorizontalScroll.Enabled = false;
            this.HorizontalScroll.Visible = false;
            this.VerticalScroll.Enabled = false;
            this.VerticalScroll.Visible = false;

            var size=new Size((int)_ltd.PxOverallWidth + this.Margin.Left + this.Margin.Right, (int)_ltd.PxOverallHeight + this.Margin.Top + this.Margin.Bottom);

            Bitmap bmp = new Bitmap(size.Width, size.Height);
            
            Rectangle rect = new Rectangle(0, 0, size.Width, size.Height);
          
            Graphics gBmp = Graphics.FromImage(bmp);

           
            

            gBmp.FillRectangle(backgroundColor, rect);
            drawNode(gBmp);
            drawConnection(gBmp);

            

            this.DrawToBitmap(bmp, rect);

            bmp.Save(path, System.Drawing.Imaging.ImageFormat.Png);

            //Restore state
            
            this.HorizontalScroll.Enabled = true;
            this.HorizontalScroll.Visible = true;
            this.VerticalScroll.Enabled = true;
            this.VerticalScroll.Visible = true;
            this.AutoScroll = true;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            backgroundColor = new SolidBrush(SystemColors.Control);
            nodeBrush = backgroundColor;
            //Redraw th control
            Invalidate(true);

        }

        #endregion

    }
}

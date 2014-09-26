using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.Xml;

namespace Client
{
	/// <summary>
	/// Description of QQListBox.
	/// </summary>
	public partial class QQListBox : ListBox
	{
		public QQListBoxItem mouseitem;
        public string _xmlpath="QQdate.xml";
		private QQListBoxItemCollection _items;
		
		public QQListBox()
            : base()
        {		
            _items = new QQListBoxItemCollection(this);
            base.DrawMode = DrawMode.OwnerDrawVariable;            
            InitializeComponent();
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);// ˫����
            this.SetStyle(ControlStyles.ResizeRedraw, true);//������Сʱ�ػ�
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true); // ��ֹ��������.
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);// ˫����            
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            //this.BackColor=Color.Transparent;
        }
        [Localizable(true)]
        [MergableProperty(false)]
        [DesignerSerializationVisibility(
            DesignerSerializationVisibility.Content)]

       public string Xmlpath
        {
            get { return _xmlpath; }
            set {_xmlpath=value; }
        }
     
       public new QQListBoxItemCollection Items
        {
            get { return _items; }
        }
      
       internal ListBox.ObjectCollection OldItems
        {
            get { return base.Items; }
        }
       
       protected override void OnPaint(PaintEventArgs e)
        {
         	//Bitmap bg=new Bitmap(this.ClientRectangle.Width,this.ClientRectangle.Height);         	
         	//Graphics g =Graphics.FromImage(bg);
         	Graphics g=e.Graphics;
         	#region ����ѡ����
       	    //�ж��Ƿ���ѡ����
            if (this.Focused && this.SelectedItem != null)
            {
            	//�õ�ѡ���������
            	Rectangle bounds = this.GetItemRectangle(this.SelectedIndex);
            	//�ж�ѡ�����Ƿ�Ϊ����
            	if (Items[this.SelectedIndex].IsClass)
            	{
            	     //g.FillRectangle(new SolidBrush(Color.FromArgb(230,238,241)),bounds);
                     //Items[this.SelectedIndex].DrawImage(global::QQListBox.Properties.Resources.MainPanel_FolderNode_collapseTexture, new Rectangle(bounds.X+3,bounds.Y+6,12,12));
                     g.DrawString(Items[this.SelectedIndex].Title, new Font("΢���ź�", 9), new SolidBrush(Color.Black), bounds.Left + 15, bounds.Top + 4);
            	}
            	else
            	{
            		g.FillRectangle(new SolidBrush(Color.FromArgb(252,233,161)),bounds);
            	}

            }
            #endregion
            
            //ѭ������ÿ��
            for (int i = 0; i < Items.Count; i++)
            {
               Rectangle bounds = this.GetItemRectangle(i) ;
               QQListBoxItem item = Items[i];               
              if (item.IsClass==true)
		      {                  
                //g.FillRectangle(new SolidBrush(Color.FromArgb(50,230,238,241)),bounds);
                //g.DrawImage(global::QQListBox.Properties.Resources.MainPanel_FolderNode_collapseTexture, new Rectangle(bounds.X+3,bounds.Y+6,12,12));
                if(mouseitem==Items[i] && mouseitem!=this.SelectedItem)
                { 
                	g.FillRectangle(new SolidBrush(Color.FromArgb(50,230,238,241)),bounds);                
                }
                 g.DrawString(item.Title, new Font("΢���ź�", 9), new SolidBrush(Color.Black), bounds.Left + 15, bounds.Top + 4);
              }
              else
              {
  
                if ( item!=this.SelectedItem)
                {

                	Color backColor = Color.FromArgb(20,216,211,211);
                    using (SolidBrush brush = new SolidBrush(backColor))
                    {
                    	//g.FillRectangle(new SolidBrush(Color.White),bounds);
                    	g.FillRectangle(brush, new Rectangle(bounds.X+1,bounds.Y+1,bounds.Width-2,bounds.Height-1));
                    }
                }
                if(mouseitem==Items[i] && mouseitem!=this.SelectedItem)
                {
                    Color backColor = Color.FromArgb(200,192,224,248);
                    using (SolidBrush brush = new SolidBrush(backColor))
                    {
                    	//g.FillRectangle(new SolidBrush(Color.White),bounds);
                    	g.FillRectangle(brush, new Rectangle(bounds.X+1,bounds.Y+1,bounds.Width-2,bounds.Height-1));
                    }
                
                }
                Image image = item.Image;
                string text = item.ToString();
              
                if (image != null)
                {
                    g.InterpolationMode =  InterpolationMode.HighQualityBilinear;
                    g.DrawImage(image,new Rectangle(bounds.X+12,bounds.Y+7,image.Size.Width,image.Size.Height));
                }
                int tw=40;
                if(item.Remarks!=null)
                {
                g.DrawString(item.Remarks,new Font("΢���ź�", 9), new SolidBrush(this.ForeColor), bounds.Left + tw + 15, bounds.Top + 4);                
                tw=item.Remarks.Length+tw+25;
               // item.Remarks.
                }
               // GetTextSize()
                //g.DrawString("(" + item.Title + ")", /*this.Font*/new Font("΢���ź�", 9), new SolidBrush(Color.FromArgb(128, 128, 128)), bounds.Left + tw + 15, bounds.Top + 4);
                g.DrawString(item.Text, new Font("΢���ź�", 9), new SolidBrush(Color.FromArgb(128, 128, 128)), bounds.Left + 40 + 15, bounds.Top + 19);

              }
            }
            base.OnPaint(e);
         //   e.Graphics.DrawImage(bg,this.ClientRectangle);
        }


      #region//������
//      protected override void OnDrawItem(DrawItemEventArgs e)
//        {
//            base.OnDrawItem(e);
//            if (e.Index != -1 && base.Items.Count > 0)
//            {
//                System.Diagnostics.Debug.WriteLine(e.State);
//                Rectangle bounds = e.Bounds;
//                QQListBoxItem item = Items[e.Index];
//                Graphics g = e.Graphics;
//              if (item.IsClass==true)
//		      {                  
//              	g.FillRectangle(new SolidBrush(Color.FromArgb(50,230,238,241)),bounds);
//               // g.DrawImage(global::QQListBox.Properties.Resources.MainPanel_FolderNode_collapseTexture, new Rectangle(bounds.X+3,bounds.Y+6,12,12));
//
//                g.DrawString(item.Title, new Font("΢���ź�", 9), new SolidBrush(Color.Black), bounds.Left + 15, bounds.Top + 4);
//              }
//              else
//              {
//  
//                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
//                {
//                    //g.DrawString(item.Title,new Font("΢���ź�", 9), new SolidBrush(Color.Black), bounds.Left + 70 + 10, bounds.Top + 4);
//                	e.Graphics.FillRectangle(new SolidBrush(Color.FromArgb(252,233,161)),bounds);
//                }
//                else
//                {
//                	Color backColor = Color.FromArgb(60,216,211,211);
//                    using (SolidBrush brush = new SolidBrush(backColor))
//                    {
//                    	g.FillRectangle(new SolidBrush(Color.White),bounds);
//                    	g.FillRectangle(brush, new Rectangle(bounds.X+1,bounds.Y+1,bounds.Width-2,bounds.Height-1));
//                    }
//                }
//
//                Image image = item.Image;
//                string text = item.ToString();
//              
//                if (image != null)
//                {
//                    g.InterpolationMode =  InterpolationMode.HighQualityBilinear;
//                    g.DrawImage(image,new Rectangle(bounds.X+12,bounds.Y+7,image.Size.Width,image.Size.Height));
//                }
//               // int Tw=0;
//                if(item.Remarks!=null)
//                {
//                g.DrawString(item.Remarks,new Font("΢���ź�", 9), new SolidBrush(this.ForeColor), bounds.Left + 40 + 15, bounds.Top + 4);                
//               // tw=item.Remarks
//                }
//                g.DrawString("(" + item.Title + ")", /*this.Font*/new Font("΢���ź�", 9), new SolidBrush(Color.FromArgb(128, 128, 128)), bounds.Left + 70 + 15, bounds.Top + 4);
//                g.DrawString(item.Text, new Font("΢���ź�", 9), new SolidBrush(Color.FromArgb(128, 128, 128)), bounds.Left + 40 + 15, bounds.Top + 19);
//
//                if ((e.State & DrawItemState.Focus) ==
//                    DrawItemState.Focus)
//                {
//                    e.DrawFocusRectangle();
//                }
//              }
//            }
//        }
	  #endregion
      protected override void OnMeasureItem(MeasureItemEventArgs e)
	  {
		base.OnMeasureItem(e);        
        try
        {
            QQListBoxItem item = Items[e.Index];
            if (item.IsClass == true)
            { e.ItemHeight = 25; }
            else
            { e.ItemHeight = 54; }
        }
        catch { }
	  }
	  protected override void OnClick(EventArgs e)
	  {
		base.OnClick(e);
     	//this.Items.Clear(); 
     	//while ( this.SelectedItems.Count > 0 )
        //this.Items.Remove ( this.SelectedItems[0] );
	  }
	  protected override void OnSelectedIndexChanged(EventArgs e)
	  {
		base.OnSelectedIndexChanged(e);
		QQListBoxItem item = Items[this.SelectedIndex];
		//MessageBox.Show(this.SelectedIndex.ToString());
        if (item.IsClass == true && item.IsExpand == true)
        {
            for (int i = this._items.Count - 1; i >= 0; i--)
            {
                if (this._items[i].Classid == item.Classid && this._items[i] != item)
                {
                    this._items.RemoveAt(i);
                }
            }
            item.IsExpand = false;
        }
        else if (item.IsClass == true)
        {
            item.IsExpand = true;
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(_xmlpath);
                XmlNodeList Qd = doc.DocumentElement.ChildNodes[item.Classid].ChildNodes;
                int i = 1;
                foreach (XmlNode Qi in Qd)
                {
                    this._items.Insert(
                        this.SelectedIndex+i,
                        new QQListBoxItem(
                            item.Classid, 
                            int.Parse(Qi.Attributes["qq"].Value),
                            Qi.Attributes["title"].Value, 
                            Qi.Attributes["remarks"].Value,
                            Qi.Attributes["text"].Value, 
                            new Bitmap(Qi.Attributes["image"].Value),
                            true, 
                            true, 
                            true));
                    i++;
   
                }

            }
            catch (Exception A)
            {
                Console.WriteLine("Exception: {0}", A.ToString());
            }  
        
        }
        this.Invalidate();
 	  }
      protected override void OnDoubleClick(EventArgs e)
      {
          base.OnDoubleClick(e);
          QQListBoxItem item = Items[this.SelectedIndex];
         // MessageBox.Show(this.SelectedIndex.ToString());
      }
	  protected override void OnMouseMove(MouseEventArgs e)
	  {
		base.OnMouseMove(e);
		for(int i = 0; i < Items.Count; i++)
		{
			Rectangle bounds = this.GetItemRectangle(i) ;
			if(bounds.Contains(e.X,e.Y))
			{				
				if (Items[i]!=mouseitem)
				{
				  mouseitem=Items[i];
				 this.Invalidate();
				} 
			}
		}		
	  }
	  
	  #region �϶�
	  //private ListBox sourcelbl;
	  protected override void OnDragOver(DragEventArgs e)
	  {
		 base.OnDragOver(e);
		             //�϶�Դ�ͷ��õ�Ŀ�ĵ�һ����һ��ListBox
            if (e.Data.GetDataPresent(typeof(System.String)))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
                e.Effect = DragDropEffects.None;


	  }
	  protected override void OnDragDrop(DragEventArgs drgevent)
	  {
		base.OnDragDrop(drgevent);
	  }
	  #endregion
	  //�����ı�����
	  private int StringLength(string text)
      {
            int len = 0;

            for (int i = 0; i < text.Length; i++)
            {
                byte[] byte_len = System.Text.Encoding.Default.GetBytes(text.Substring(i, 1));
                if (byte_len.Length > 1)
                    len += 2;  //������ȴ���1�������ģ�ռ�����ֽڣ�+2
                else
                    len += 1;  //������ȵ���1����Ӣ�ģ�ռһ���ֽڣ�+1
            }

            return len;
       }
	}
}

#region ListBo��һЩ�÷�
//1. �����б�

//    SelectionMode    �������Ŀ��ѡ�����ͣ�����ѡ(Multiple)����ѡ(Single)
//    Rows             �б������ʾ�ܹ�������
//    Selected         �����Ŀ�Ƿ�ѡ��
//    SelectedItem     ���ص�������ListItem������б���б�ѡ�����Ŀ
//    Count            �б������Ŀ������
//    SelectedIndex    �б���б�ѡ���������ֵ
//    Items            ��ָ�б���е������ÿһ������Ͷ���ListItem

//2. ȡ�б���б�ѡ�е�ֵ  

//     ListBox.SelectedValue  

//3. ��̬������б���е��

//     ListBox.Items.Add("��Ҫ��ӵ���");

//4. �Ƴ�ָ���

//     //�����ж��б���е����Ƿ����0
//     If(ListBox.Items.Count > 0 )
//     {
////�Ƴ�ѡ�����
//ListBox.Items.Remove(ListBox.SelectedItem);
//     }

//5. ��������

//     //�����ж��б���е����Ƿ����0
//     If(ListBox.Items.Count > 0 )
//     {
////���������
//ListBox.Items.Clear();
//     }

//6. �б�����һ��ѡ����
   
//     ֻ�������б������� SelectionMode="Multiple",��Ctrl���Զ�ѡ

//7. �����б�������������������˵�

//     //�жϵ�һ���б���б�ѡ�е�ֵ
//     switch(ListBox1.SelectValue)
//     {
////�����"A"���ڶ����б���о������Щ��
//case "A"
//      ListBox2.Items.Clear();
//      ListBox2.Items.Add("A1");
//      ListBox2.Items.Add("A2");
//      ListBox2.Items.Add("A3");
////�����"B"���ڶ����б���о������Щ��
//case "B"
//      ListBox2.Items.Clear();
//      ListBox2.Items.Add("B1");
//      ListBox2.Items.Add("B2");
//      ListBox2.Items.Add("B3");
//     }

//8. ʵ���б���������λ
//     ����������λ��������λ
//     �����˼·Ϊ������һ��ListBox���󣬲���Ҫ��λ�������ݷ�����������С�
//     �����������λ�����ǰѵ�ǰѡ����ĵ���һ���ֵ������ǰѡ�����Ȼ��
//     �Ѹղ��¼���Ķ����ֵ���ٸ�����ǰѡ�����ǰһ�
//     �������Ϊ��
//      //����һ������������λ��
//      index = -1;
//      //����ǰ��Ŀ���ı��Լ�ֵ�����浽һ����ʱ��������
//      ListItem lt=new ListItem (ListBox.SelectedItem.Text,ListBox.SelectedValue);
//      //��ѡ�е����ֵ������һ������һ����ֵ
//      ListBox.Items[ListBox.SelectedIndex].Text=ListBox.Items[ListBox.SelectedIndex + index].Text;
//      //��ѡ�е����ֵ������һ������һ����ֵ
//      ListBox.Items[ListBox.SelectedIndex].Value=ListBox.Items[ListBox.SelectedIndex + index].Value;
//      //�ѱ�ѡ�����ǰһ������һ����ֵ����ʱ�����е�ȡ��
//      ListBox.Items[ListBox.SelectedIndex].Test=lt.Test;
//      //�ѱ�ѡ�����ǰһ������һ����ֵ����ʱ�����е�ȡ��
//      ListBox.Items[ListBox.SelectedIndex].Value=lt.Value;
//      //�����ָ��ŵ��ƶ����������
//      ListBox.Items[ListBox.SelectedIndex].Value=lt.Value;

//9. �ƶ�ָ�뵽ָ��λ�ã�

//      (1).��������
//          //����ѡ�������������Ϊ0��OK��
//          ListBox.SelectIndex=0;
//      (2).����β��
//          //����ѡ�������������ΪListBox.Items.Count-1��OK��
//          ListBox.SelectIndex=ListBox.Items.Count-1;
//      (3).��һ��
//          //�õ�ǰ��ѡ�е�����ȥ�� 1
//          ListBox.SelectIndex=ListBox.SelectIndex - 1;
//      (4).��һ��
//          //�õ�ǰ��ѡ�е�����ȥ�� 1
//          ListBox.SelectIndex=ListBox.SelectIndex + 1;

//this.ListBox1.Items.Insertat(3,new   ListItem("�����ڵ�3��֮����",""));  

//this.ListBox1.Items.Insertat(index,ListItem)

//ListBox1.Items.Insert(0,new   ListItem("text","value"));
#endregion

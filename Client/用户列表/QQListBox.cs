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
            this.SetStyle(ControlStyles.DoubleBuffer, true);// 双缓冲
            this.SetStyle(ControlStyles.ResizeRedraw, true);//调整大小时重绘
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);// 双缓冲            
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
         	#region 绘制选中项
       	    //判断是否有选中项
            if (this.Focused && this.SelectedItem != null)
            {
            	//得到选中项的区域
            	Rectangle bounds = this.GetItemRectangle(this.SelectedIndex);
            	//判断选中项是否为分类
            	if (Items[this.SelectedIndex].IsClass)
            	{
            	     //g.FillRectangle(new SolidBrush(Color.FromArgb(230,238,241)),bounds);
                     //Items[this.SelectedIndex].DrawImage(global::QQListBox.Properties.Resources.MainPanel_FolderNode_collapseTexture, new Rectangle(bounds.X+3,bounds.Y+6,12,12));
                     g.DrawString(Items[this.SelectedIndex].Title, new Font("微软雅黑", 9), new SolidBrush(Color.Black), bounds.Left + 15, bounds.Top + 4);
            	}
            	else
            	{
            		g.FillRectangle(new SolidBrush(Color.FromArgb(252,233,161)),bounds);
            	}

            }
            #endregion
            
            //循环绘制每项
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
                 g.DrawString(item.Title, new Font("微软雅黑", 9), new SolidBrush(Color.Black), bounds.Left + 15, bounds.Top + 4);
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
                g.DrawString(item.Remarks,new Font("微软雅黑", 9), new SolidBrush(this.ForeColor), bounds.Left + tw + 15, bounds.Top + 4);                
                tw=item.Remarks.Length+tw+25;
               // item.Remarks.
                }
               // GetTextSize()
                //g.DrawString("(" + item.Title + ")", /*this.Font*/new Font("微软雅黑", 9), new SolidBrush(Color.FromArgb(128, 128, 128)), bounds.Left + tw + 15, bounds.Top + 4);
                g.DrawString(item.Text, new Font("微软雅黑", 9), new SolidBrush(Color.FromArgb(128, 128, 128)), bounds.Left + 40 + 15, bounds.Top + 19);

              }
            }
            base.OnPaint(e);
         //   e.Graphics.DrawImage(bg,this.ClientRectangle);
        }


      #region//绘制项
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
//                g.DrawString(item.Title, new Font("微软雅黑", 9), new SolidBrush(Color.Black), bounds.Left + 15, bounds.Top + 4);
//              }
//              else
//              {
//  
//                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
//                {
//                    //g.DrawString(item.Title,new Font("微软雅黑", 9), new SolidBrush(Color.Black), bounds.Left + 70 + 10, bounds.Top + 4);
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
//                g.DrawString(item.Remarks,new Font("微软雅黑", 9), new SolidBrush(this.ForeColor), bounds.Left + 40 + 15, bounds.Top + 4);                
//               // tw=item.Remarks
//                }
//                g.DrawString("(" + item.Title + ")", /*this.Font*/new Font("微软雅黑", 9), new SolidBrush(Color.FromArgb(128, 128, 128)), bounds.Left + 70 + 15, bounds.Top + 4);
//                g.DrawString(item.Text, new Font("微软雅黑", 9), new SolidBrush(Color.FromArgb(128, 128, 128)), bounds.Left + 40 + 15, bounds.Top + 19);
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
	  
	  #region 拖动
	  //private ListBox sourcelbl;
	  protected override void OnDragOver(DragEventArgs e)
	  {
		 base.OnDragOver(e);
		             //拖动源和放置的目的地一定是一个ListBox
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
	  //计算文本长度
	  private int StringLength(string text)
      {
            int len = 0;

            for (int i = 0; i < text.Length; i++)
            {
                byte[] byte_len = System.Text.Encoding.Default.GetBytes(text.Substring(i, 1));
                if (byte_len.Length > 1)
                    len += 2;  //如果长度大于1，是中文，占两个字节，+2
                else
                    len += 1;  //如果长度等于1，是英文，占一个字节，+1
            }

            return len;
       }
	}
}

#region ListBo的一些用法
//1. 属性列表：

//    SelectionMode    组件中条目的选择类型，即多选(Multiple)、单选(Single)
//    Rows             列表框中显示总共多少行
//    Selected         检测条目是否被选中
//    SelectedItem     返回的类型是ListItem，获得列表框中被选择的条目
//    Count            列表框中条目的总数
//    SelectedIndex    列表框中被选择项的索引值
//    Items            泛指列表框中的所有项，每一项的类型都是ListItem

//2. 取列表框中被选中的值  

//     ListBox.SelectedValue  

//3. 动态的添加列表框中的项：

//     ListBox.Items.Add("所要添加的项");

//4. 移出指定项：

//     //首先判断列表框中的项是否大于0
//     If(ListBox.Items.Count > 0 )
//     {
////移出选择的项
//ListBox.Items.Remove(ListBox.SelectedItem);
//     }

//5. 清空所有项：

//     //首先判断列表框中的项是否大于0
//     If(ListBox.Items.Count > 0 )
//     {
////清空所有项
//ListBox.Items.Clear();
//     }

//6. 列表框可以一次选择多项：
   
//     只需设置列表框的属性 SelectionMode="Multiple",按Ctrl可以多选

//7. 两个列表框联动，即两级联动菜单

//     //判断第一个列表框中被选中的值
//     switch(ListBox1.SelectValue)
//     {
////如果是"A"，第二个列表框中就添加这些：
//case "A"
//      ListBox2.Items.Clear();
//      ListBox2.Items.Add("A1");
//      ListBox2.Items.Add("A2");
//      ListBox2.Items.Add("A3");
////如果是"B"，第二个列表框中就添加这些：
//case "B"
//      ListBox2.Items.Clear();
//      ListBox2.Items.Add("B1");
//      ListBox2.Items.Add("B2");
//      ListBox2.Items.Add("B3");
//     }

//8. 实现列表框中项的移位
//     即：向上移位、向下移位
//     具体的思路为：创建一个ListBox对象，并把要移位的项先暂放在这个对象中。
//     如果是向上移位，就是把当前选定项的的上一项的值赋给当前选定的项，然后
//     把刚才新加入的对象的值，再附给当前选定项的前一项。
//     具体代码为：
//      //定义一个变量，作移位用
//      index = -1;
//      //将当前条目的文本以及值都保存到一个临时变量里面
//      ListItem lt=new ListItem (ListBox.SelectedItem.Text,ListBox.SelectedValue);
//      //被选中的项的值等于上一条或下一条的值
//      ListBox.Items[ListBox.SelectedIndex].Text=ListBox.Items[ListBox.SelectedIndex + index].Text;
//      //被选中的项的值等于上一条或下一条的值
//      ListBox.Items[ListBox.SelectedIndex].Value=ListBox.Items[ListBox.SelectedIndex + index].Value;
//      //把被选中项的前一条或下一条的值用临时变量中的取代
//      ListBox.Items[ListBox.SelectedIndex].Test=lt.Test;
//      //把被选中项的前一条或下一条的值用临时变量中的取代
//      ListBox.Items[ListBox.SelectedIndex].Value=lt.Value;
//      //把鼠标指针放到移动后的那项上
//      ListBox.Items[ListBox.SelectedIndex].Value=lt.Value;

//9. 移动指针到指定位置：

//      (1).移至首条
//          //将被选中项的索引设置为0就OK了
//          ListBox.SelectIndex=0;
//      (2).移至尾条
//          //将被选中项的索引设置为ListBox.Items.Count-1就OK了
//          ListBox.SelectIndex=ListBox.Items.Count-1;
//      (3).上一条
//          //用当前被选中的索引去减 1
//          ListBox.SelectIndex=ListBox.SelectIndex - 1;
//      (4).下一条
//          //用当前被选中的索引去加 1
//          ListBox.SelectIndex=ListBox.SelectIndex + 1;

//this.ListBox1.Items.Insertat(3,new   ListItem("插入在第3行之后项",""));  

//this.ListBox1.Items.Insertat(index,ListItem)

//ListBox1.Items.Insert(0,new   ListItem("text","value"));
#endregion

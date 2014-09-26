using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Drawing;
using System.ComponentModel;

namespace Client
{
[Serializable]
[TypeConverter(typeof(ExpandableObjectConverter))]
public class QQListBoxItem : IDisposable
    {
	    private bool _isClass=false;
        private bool _isExpand=false;
	    private int _classid=0;
        private int _qq = 646494711;
        private string _title ="昵称";
        private string _remarks = "备注";
        private string _text = "个性签名";
        private Image _image;
        private enum _status
        {
        	online,
        	unline
        }
        private bool _isE_mail;
        private bool _isSms;
        private bool _isVideo;
        private object _tag;
    
       #region 构造函数
        public QQListBoxItem()
        {
        }
     
        /// <summary>
        /// QQ好友分类
        /// </summary>
        /// <param name="IsClass">是否为类别</param>
        /// <param name="classid">类别ID</param>
        /// <param name="title">分类名</param>
        /// <param name="isExpand">是否展开</param>
        public QQListBoxItem(
            bool isClass, 
            int classid, 
            string title, 
            bool isExpand)
        {
            _isClass = isClass;
            _classid = classid;
            _title = title;
            _isExpand = isExpand;
        }

    /// <summary>
    /// QQLlitBox子项
    /// </summary>
    /// <param name="IsClass">是否为类别</param>
    /// <param name="classid">类别ID</param>
    /// <param name="qq">QQ</param>
    /// <param name="title">标题</param>
    /// <param name="remarks">备注</param>
    /// <param name="text">说明</param>
    /// <param name="image">图标</param>
    /// <param name="isE_mail">是否显示E-mail</param>
    /// <param name="isSms">是否显示语言</param>
    /// <param name="isVideo">是否显示视频</param>
        public QQListBoxItem(
	                    int classid,
                        int qq,
                        string title,
                        string remarks,
                        string text,
                        Image image,
                      //  _status status,
                        bool isE_mail,
                        bool isSms,
                        bool isVideo)
        {
	        _classid=classid;
            _qq = qq;
            _remarks = remarks;
            _title = title;
            _text = text;
            _image = image;
           // _status = status;
            _isE_mail = isE_mail;
            _isSms = isSms;
            _isVideo = isVideo;
 
        }

       #endregion

       #region 属性
        public bool IsClass
        {
            get{ return _isClass; }
            set{ _isClass = value; }
        }
        public bool IsExpand
        {
            get { return _isExpand; }
            set { _isExpand = value; }
        }

        public int Classid
        {
            get{ return _classid; }
            set{ _classid = value; }
        }

        [DefaultValue(typeof(Image), "null")]
        public Image Image
        {
            get{ return _image; }
            set{ _image = value; }
        }
        public int QQ
        {
            get{ return _qq; }
            set{ _qq = value; }
        }
        [DefaultValue("ImageComboBoxItem")]
        [Localizable(true)]
        public string Title
        {
            get{ return _title; }
            set{ _title = value; }
        }
        public string Remarks
        {
            get{ return _remarks; }
            set{ _remarks = value; }
        }

        public string Text
        {
            get{ return _text; }
            set{ _text = value; }
        }
        public bool IsE_mail
        {
            get{ return _isE_mail; }
            set{ _isE_mail = value; }
        }
        public bool IsSms
        {
            get{ return _isSms; }
            set{ _isSms = value; }
        }
        public bool isVideo
        {
            get{ return _isVideo; }
            set{ _isVideo = value; }
        }

        [Bindable(true)]
        [Localizable(false)]
        [DefaultValue("")]
        [TypeConverter(typeof(StringConverter))]
        [DesignerSerializationVisibility(
            DesignerSerializationVisibility.Hidden)]
        public object Tag
        {
            get{ return _tag; }
            set{ _tag = value; }
        }

       #endregion

       #region 重写方法

        public override string ToString()
        {
            return _text;
        }

        #endregion

       #region IDisposable 成员

        public void Dispose()
        {
            _image = null;
            _tag = null;
        }

        #endregion
}

}

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
        private string _title ="�ǳ�";
        private string _remarks = "��ע";
        private string _text = "����ǩ��";
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
    
       #region ���캯��
        public QQListBoxItem()
        {
        }
     
        /// <summary>
        /// QQ���ѷ���
        /// </summary>
        /// <param name="IsClass">�Ƿ�Ϊ���</param>
        /// <param name="classid">���ID</param>
        /// <param name="title">������</param>
        /// <param name="isExpand">�Ƿ�չ��</param>
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
    /// QQLlitBox����
    /// </summary>
    /// <param name="IsClass">�Ƿ�Ϊ���</param>
    /// <param name="classid">���ID</param>
    /// <param name="qq">QQ</param>
    /// <param name="title">����</param>
    /// <param name="remarks">��ע</param>
    /// <param name="text">˵��</param>
    /// <param name="image">ͼ��</param>
    /// <param name="isE_mail">�Ƿ���ʾE-mail</param>
    /// <param name="isSms">�Ƿ���ʾ����</param>
    /// <param name="isVideo">�Ƿ���ʾ��Ƶ</param>
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

       #region ����
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

       #region ��д����

        public override string ToString()
        {
            return _text;
        }

        #endregion

       #region IDisposable ��Ա

        public void Dispose()
        {
            _image = null;
            _tag = null;
        }

        #endregion
}

}

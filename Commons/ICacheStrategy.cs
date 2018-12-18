using System;
using System.Text;

    /// <summary>
    /// ����������Խӿ�
    /// </summary>
    public interface ICacheStrategy
    {
        /// <summary>
        /// ���ָ��ID�Ķ���
        /// </summary>
        /// <param name="objId"></param>
        /// <param name="o"></param>
        void AddObject(string objId, object o);

        /// <summary>
        /// ���ָ��ID�Ķ���(����ָ���ļ���)
        /// </summary>
        /// <param name="objId"></param>
        /// <param name="o"></param>
        /// <param name="files"></param>
        void AddObjectWithFileChange(string objId, object o, string[] files);

        /// <summary>
        /// ���ָ��ID�Ķ���(����ָ����ֵ��)
        /// </summary>
        /// <param name="objId"></param>
        /// <param name="o"></param>
        /// <param name="dependKey"></param>
        void AddObjectWithDepend(string objId, object o, string[] dependKey);

        /// <summary>
        /// �Ƴ�ָ��ID�Ķ���
        /// </summary>
        /// <param name="objId"></param>
        void RemoveObject(string objId);

        /// <summary>
        /// ����ָ��ID�Ķ���
        /// </summary>
        /// <param name="objId"></param>
        /// <returns></returns>
        object RetrieveObject(string objId);

        /// <summary>
        /// ����ʱ��
        /// </summary>
        int TimeOut { set;get;}
   }


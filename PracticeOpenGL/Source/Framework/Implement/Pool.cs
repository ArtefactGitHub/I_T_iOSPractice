using System.Collections.Generic;
using System.Linq;

namespace PracticeOpenGL.Source.Framework.Implement
{
    public interface PoolObjectFactory<T> where T : new()
    {
        T CreateObject();
    }

    public class Pool<T> where T : new()
    {
        readonly List<T> m_FreeObjects;

        readonly PoolObjectFactory<T> m_Factory;

        readonly int m_MaxSize;

        public Pool(PoolObjectFactory<T> factory, int maxSize)
        {
            this.m_Factory = factory;
            this.m_MaxSize = maxSize;
            this.m_FreeObjects = new List<T>(maxSize);
        }

        public T NewObject()
        {
            T result = default(T);

            if (m_FreeObjects.Count == 0)
            {
                result = m_Factory.CreateObject();
            }
            else
            {
                result = m_FreeObjects.Last<T>();
                m_FreeObjects.RemoveAt(m_FreeObjects.Count - 1);
            }

            return result;
        }

        public void Free(T obj)
        {
            if (m_FreeObjects.Count < m_MaxSize)
            {
                m_FreeObjects.Add(obj);
            }
        }
    }
}

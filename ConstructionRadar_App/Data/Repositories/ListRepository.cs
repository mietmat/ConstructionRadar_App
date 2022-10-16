﻿using ConstructionRadar_App.Entities;

namespace ConstructionRadar_App.Repositories
{
    //public delegate void ItemAdded(object item);
    public class ListRepository<T> : IRepository<T>
        where T : class, IEntity, new()
    {
        protected readonly List<T> _items = new();

        public void Add(T item)
        {
            item.Id = _items.Count + 1;
            _items.Add(item);

        }

        public void Remove(T item)
        {
            _items.Remove(item);
        }

        public T GetById(int id)
        {
            return _items.Single(e => e.Id == id);
        }

        public IEnumerable<T> GetAll()
        {
            return _items.ToList();
        }

        public void Save()
        {
            foreach (var item in _items)
            {
                Console.WriteLine(item);
            }
        }

    }

}
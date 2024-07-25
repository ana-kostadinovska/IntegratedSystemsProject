﻿using BookStore.Domain.Models;
using BookStore.Repository.Interface;
using BookStore.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Implementation
{
    public class PublisherService : IPublisherService
    {
        private readonly IRepository<Publisher> _publisherRepository;

        public PublisherService(IRepository<Publisher> publisherRepository)
        {
            _publisherRepository = publisherRepository;
        }

        public void CreateNewPublisher(Publisher publisher)
        {
            _publisherRepository.Insert(publisher);
        }

        public void DeletePublisher(Guid id)
        {
            Publisher pubishlerToDelete = _publisherRepository.Get(id);
            _publisherRepository.Delete(pubishlerToDelete);

        }

        public void EditExistingPublisher(Publisher publisher)
        {
            _publisherRepository.Update(publisher);
        }

        public List<Publisher> GetAllPublishers()
        {
            return _publisherRepository.GetAll().ToList();
        }

        public Publisher GetDetailsForPublisher(Guid? id)
        {
            return _publisherRepository.Get(id);
        }
    }
}
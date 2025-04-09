﻿using BiblioTDD.Models.Domain;
using BiblioTDD.Models.Exceptions;
using BiblioTDD.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BiblioTDD.app
{
    public class BookController
    {
        private readonly IBookService _service;

        public BookController(IBookService service)
        {
            this._service = service;
        }

        public bool RegisterBook(Book book) 
        {
            if (_service.GetById(book.BookId) != null) { 
                throw new ISBNDuplicateException(); }

            if(string.IsNullOrEmpty(book.ISBN) || string.IsNullOrEmpty(book.Title) ) {
                throw new MandatoryFieldMissingException(); }

            if (book.Copies < 0)
            {
                throw new NegativeCopiesException(); }

            try
            {
                _service.AddBook(book);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }


        public bool UpdateBook(Book book)
        {
            var existing = _service.GetById(book.BookId);
            if (existing == null)
                throw new Exception("Book not found");

            try
            {
                _service.UpdateBook(book);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}

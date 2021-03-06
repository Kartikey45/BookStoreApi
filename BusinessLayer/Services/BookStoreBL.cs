﻿using BusinessLayer.Interface;
using CommonLayer.BookStoreModel;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class BookStoreBL : IBookStoreBL
    {
        //Initialise variable 
        private readonly IBookStoreRL BookDetails;

        //constructore declare
        public BookStoreBL(IBookStoreRL BookDetails)
        {
            this.BookDetails = BookDetails;
        }

        //Method to insert book
        public Storedetails InsertBooks(BookStoreDetails details)
        {
            try
            {
                var result = BookDetails.InsertBooks(details);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Get all Books details
        public List<Storedetails> ViewAllBooks()
        {
            try
            {
                var result = BookDetails.ViewAllBooks();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Method to sort By book details
        public List<Storedetails> SortByBookDetails(string columnName, string order)
        {
            try
            {
                var data = BookDetails.SortByBookDetails(columnName, order);
                return data;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Delete Book By Id
        public Response DeleteBook(int BookId)
        {
           try
            {
                var result = BookDetails.DeleteBook(BookId);
                return result;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Method to update book details
        public Storedetails UpdateBooks(int BookId, UpdateBookDetails data)
        {
            try
            {
                var result = BookDetails.UpdateBooks(BookId, data);
                return result;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Method to serach book
        public List<Storedetails> BookSearch(string search)
        {
            try
            {
                var data = BookDetails.BookSearch(search);
                return data;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Insert image
        public Storedetails InsertImage(IFormFile BookImage, int BookId)
        {
            try
            {
                var data = BookDetails.InsertImage(BookImage,BookId);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

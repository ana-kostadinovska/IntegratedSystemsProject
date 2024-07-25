﻿using BookStore.Domain.DTO;
using BookStore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Interface
{
    public interface IShoppingCartService
    {
        ShoppingCartDto getShoppingCartInfo(string userId);
        bool deleteProductFromShoppingCart(string userId, Guid bookId);
        bool order(string userId);
        bool AddToShoppingConfirmed(BookInShoppingCart model, string userId);
    }
}
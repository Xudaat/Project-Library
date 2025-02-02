﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project___ConsoleApp.Services.Interface
{
   public interface ILoanService
    {

        void Add(CreateLoanDTO createLoanDTO);
        void Update(int Id, UpdateLoanDTO updateLoanDTO);
        void Delete(int id);
        List<GetAllLoanDTO> GetAllLoanItems();
    }
}

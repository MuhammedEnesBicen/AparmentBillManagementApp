﻿namespace Entity.ViewModels
{
    public class ApartmentVM
    {
        public Apartment Apartment { get; set; }
        public int TenantId { get; set; }
        public string TenantName { get; set; }
        public float DebtAmount { get; set; }
    }
}

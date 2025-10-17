using System;
using Modules.Content.Characters.Employer.Code.Controller;
using Modules.Content.Characters.Employer.View;
using Modules.Content.FoodCollection;
using UnityEngine;

namespace Modules.Content.Characters.Employer.Events
{
    public static class EventsEmployer
    {
        public static event Action<ControllerEmployer,FoodType> OnEmployerStartCook;
        public static event Action<ViewFood,int> OnEmployerSellFood;
        public static event Action<ControllerEmployer> OnEmployerFinishedWork;

        public static void ExecuteEventOnStartCook(ControllerEmployer controllerEmployer,FoodType foodType) =>
            OnEmployerStartCook?.Invoke(controllerEmployer,foodType);
        
        public static void ExecuteEventOnEmployerSellFood(ViewFood viewFood, int profit) => 
            OnEmployerSellFood?.Invoke(viewFood, profit);
        
        public static void ExecuteOnEmployerFinishedWork(ControllerEmployer controller) =>
            OnEmployerFinishedWork?.Invoke(controller);
    }
}
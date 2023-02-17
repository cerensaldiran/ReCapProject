﻿using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspect.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        [SecuredOperation("car.add, admin")]

        [ValidationAspect(typeof(CarValidator))]

        public IResult Add(Car entity)
        {

                _carDal.Add(entity);
                return new SuccessResult(Messages.CarAdded);
     
        }

        public IResult Delete(Car entity)
        {

            _carDal.Delete(entity);
            return new SuccessResult(Messages.CarDeleted);
        }

        public IDataResult<List<Car>> GetAll()
        {
            if (DateTime.Now.Hour==17)
            {
                return new ErrorDataResult<List<Car>>(Messages.CarNameInvalid);
            }
            else
            {
                return new SuccessDataResult<List<Car>>(_carDal.GetAll(), Messages.CarsListed);
            }
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails(),Messages.CarsListed);
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int id)
        {
            return new SuccessDataResult<List<Car>> (_carDal.GetAll(c=>c.BrandId==id));
        }

        public IDataResult<List<Car>> GetCarsByColorId(int id)
        {
            return new SuccessDataResult<List< Car >>( _carDal.GetAll(c=>c.ColorId==id));
        }

        public IResult Update(Car entity)
        {
             _carDal.Update(entity);
            return new SuccessResult(Messages.CarUpdated);
        }
        public IDataResult<Car> GetById(int id)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.Id == id));
        }
    }
}

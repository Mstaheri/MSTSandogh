﻿using Application.Data.MoqData;
using Application.Services;
using Application.Services.UserAndNumberOfShares.Commands.AddUserAndNumberOfShare;
using Application.Services.UserAndNumberOfShares.Commands.DeleteUserAndNumberOfShare;
using Application.Services.UserAndNumberOfShares.Commands.UpdateUserAndNumberOfShare;
using Application.Services.UserAndNumberOfShares.Queries.GetAllUserAndNumberOfShare;
using Application.Services.UserAndNumberOfShares.Queries.GetByNameBankAndUserName;
using Application.Services.UserAndNumberOfShares.Queries.GetByUserName;
using Application.Services.UserAndNumberOfShares.Queries.GetUserAndNumberOfShare;
using Application.UnitOfWork;
using Domain.Entity;
using Domain.Exceptions;
using Domain.IRepositories;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.test.Services
{
    public class UserAndNumberOfShareTest
    {
        private readonly UserAndNumberOfShareMoqData _moqData;
        private readonly Mock<IUserAndNumberOfShareRepositorie> _repositorMoq;
        private readonly Mock<IUnitOfWork> _unitOfWorkMoq;
        public UserAndNumberOfShareTest()
        {
            _moqData = new UserAndNumberOfShareMoqData();
            _repositorMoq = new Mock<IUserAndNumberOfShareRepositorie>();
            _unitOfWorkMoq = new Mock<IUnitOfWork>();
        }
        [Fact]
        [Trait("Services", "User")]
        public async Task AddTestAsync()
        {
            Mock<ILogger<AddUserAndNumberOfShareCommandHandler>> _loggerMoq = new Mock<ILogger<AddUserAndNumberOfShareCommandHandler>>();
            var data = await _moqData.Get();
            _repositorMoq.Setup(repo => repo.AddAsync(It.IsAny<UserAndNumberOfShare>() , It.IsAny<CancellationToken>()))
                .Returns(() => ValueTask.CompletedTask);
            AddUserAndNumberOfShareCommandHandler userAndNumberOfShare = new AddUserAndNumberOfShareCommandHandler(_unitOfWorkMoq.Object
                , _repositorMoq.Object
                , _loggerMoq.Object);


            var addUserAndNumberOfShareCommand = new AddUserAndNumberOfShareCommand()
            {
                NameBankSafe = data.NameBankSafe,
                UserName = data.UserName,
                NumberOfShares = data.NumberOfShares,
            };
            var result = await userAndNumberOfShare.Handle(addUserAndNumberOfShareCommand , It.IsAny<CancellationToken>());


            Assert.NotNull(result);
            Assert.IsType<OperationResult>(result);
            if (result.IsSuccess)
            {
                Assert.Null(result.Message);
            }
            else
            {
                Assert.NotNull(result.Message);
            }
        }

        [Theory]
        [Trait("Services", "UserAndNumberOfShare")]
        [InlineData("Omid","Mstaheri")]
        [InlineData("","Estaheri")]
        [InlineData("MST", "لیبل")]
        public async Task DeleteTestAsync(string nameBankSafe ,string userName)
        {
            Mock<ILogger<DeleteUserAndNumberOfShareCommandHandler>> _loggerMoq = new Mock<ILogger<DeleteUserAndNumberOfShareCommandHandler>>();
            _repositorMoq.Setup(repo => repo.DeleteAsync(It.IsAny<Name>(),It.IsAny<UserName>(), It.IsAny<CancellationToken>()))
                .Returns(() => Task.CompletedTask);
            DeleteUserAndNumberOfShareCommandHandler userAndNumberOfShare = new DeleteUserAndNumberOfShareCommandHandler(_unitOfWorkMoq.Object
                , _repositorMoq.Object
                , _loggerMoq.Object);


            var deleteUserAndNumberOfShareCommand = new DeleteUserAndNumberOfShareCommand()
            { NameBankSafe= nameBankSafe, UserName = userName };
            var result = await userAndNumberOfShare.Handle(deleteUserAndNumberOfShareCommand , It.IsAny<CancellationToken>());


            Assert.NotNull(result);
            Assert.IsType<OperationResult>(result);
            if (result.IsSuccess)
            {
                Assert.Null(result.Message);
            }
            else
            {
                Assert.NotNull(result.Message);
            }
        }

        [Fact]
        [Trait("Services", "UserAndNumberOfShare")]
        public async Task UpdateTestAsync()
        {
            Mock<ILogger<UpdateUserAndNumberOfShareCommandHandler>> _loggerMoq = new Mock<ILogger<UpdateUserAndNumberOfShareCommandHandler>>();
            var data = await _moqData.Get();
            _repositorMoq.Setup(repo => repo.GetNameBankAndUserNameAsync
            (It.IsAny<Name>() , It.IsAny<UserName>(), It.IsAny<CancellationToken>()))
                .Returns(_moqData.Get());
            UpdateUserAndNumberOfShareCommandHandler userAndNumberOfShare = new UpdateUserAndNumberOfShareCommandHandler(_unitOfWorkMoq.Object
                , _repositorMoq.Object
                , _loggerMoq.Object);


            var updateUserAndNumberOfShareCommand = new UpdateUserAndNumberOfShareCommand()
            {
                NameBankSafe = data.NameBankSafe,
                UserName = data.UserName,
                NumberOfShares = data.NumberOfShares,
            };
            var result = await userAndNumberOfShare.Handle(updateUserAndNumberOfShareCommand , It.IsAny<CancellationToken>());


            Assert.NotNull(result);
            Assert.IsType<OperationResult>(result);
            if (result.IsSuccess)
            {
                Assert.Null(result.Message);
            }
            else
            {
                Assert.NotNull(result.Message);
            }
        }

        [Fact]
        [Trait("Services", "UserAndNumberOfShare")]
        public async Task GetAllTestAsync()
        {
            Mock<ILogger<GetAllUserAndNumberOfShareQueryHandler>> _loggerMoq = new Mock<ILogger<GetAllUserAndNumberOfShareQueryHandler>>();
            _repositorMoq.Setup(repo => repo.GetAllAsync(It.IsAny<CancellationToken>()))
                .Returns(_moqData.GetAll());
            GetAllUserAndNumberOfShareQueryHandler userAndNumberOfShare = new GetAllUserAndNumberOfShareQueryHandler(
                 _repositorMoq.Object
                , _loggerMoq.Object);


            var getAllUserAndNumberOfShareQuery = new GetAllUserAndNumberOfShareQuery();
            var result = await userAndNumberOfShare.Handle(getAllUserAndNumberOfShareQuery , It.IsAny<CancellationToken>());


            Assert.IsType<OperationResult<List<UserAndNumberOfShare>>>(result);
            if (result.IsSuccess)
            {
                Assert.Null(result.Message);

            }
            else
            {
                Assert.NotNull(result.Message);
                Assert.Null(result.Data);
            }
        }
        [Theory]
        [Trait("Service", "UserAndNumberOfShare")]
        [InlineData("estaheri")]
        [InlineData("")]
        [InlineData("MSI")]
        [InlineData("لیبل")]
        public async Task GetUserNameTestAsync(string userName)
        {
            Mock<ILogger<GetByUserNameQueryHandler>> _loggerMoq = new Mock<ILogger<GetByUserNameQueryHandler>>();
            _repositorMoq.Setup(p => p.GetUserNameAsync(It.IsAny<UserName>(), It.IsAny<CancellationToken>()))
                .Returns(_moqData.Get());
            GetByUserNameQueryHandler userAndNumberOfShare = new GetByUserNameQueryHandler(
                 _repositorMoq.Object
                , _loggerMoq.Object);


            var getByUserNameQuery = new GetByUserNameQuery()
            {
                UserName = userName,
            };
            var result = await userAndNumberOfShare.Handle(getByUserNameQuery, It.IsAny<CancellationToken>());


            Assert.IsType<OperationResult<UserAndNumberOfShare>>(result);
            if (result.IsSuccess)
            {
                Assert.Null(result.Message);

            }
            else
            {
                Assert.NotNull(result.Message);
                Assert.Null(result.Data);
            }

        }
        [Theory]
        [Trait("Service", "UserAndNumberOfShare")]
        [InlineData("estaheri")]
        [InlineData("")]
        [InlineData("MSI")]
        [InlineData("محمد")]
        public async Task GetNameBankTestAsync(string name)
        {
            Mock<ILogger<GetByNameBankQueryHandler>> _loggerMoq = new Mock<ILogger<GetByNameBankQueryHandler>>();
            _repositorMoq.Setup(p => p.GetNameBankAsync(It.IsAny<Name>(), It.IsAny<CancellationToken>()))
                .Returns(_moqData.Get());
            GetByNameBankQueryHandler userAndNumberOfShare = new GetByNameBankQueryHandler(
                 _repositorMoq.Object
                , _loggerMoq.Object);


            var getByNameBankQuery = new GetByNameBankQuery()
            { NameBankSafe= name };
            var result = await userAndNumberOfShare.Handle(getByNameBankQuery , It.IsAny<CancellationToken>());


            Assert.IsType<OperationResult<UserAndNumberOfShare>>(result);
            if (result.IsSuccess)
            {
                Assert.Null(result.Message);

            }
            else
            {
                Assert.NotNull(result.Message);
                Assert.Null(result.Data);
            }

        }
        [Theory]
        [Trait("Service", "UserAndNumberOfShare")]
        [InlineData("سعدابادی","estaheri")]
        [InlineData("salman","")]
        [InlineData("","MSI")]
        [InlineData("mamad","لیبل")]
        public async Task GetNameBankAndUserNameTestAsync(string nameBankSafe ,string userName)
        {
            Mock<ILogger<GetByNameBankAndUserNameQueryHandler>> _loggerMoq = new Mock<ILogger<GetByNameBankAndUserNameQueryHandler>>();
            _repositorMoq.Setup(p => p.GetNameBankAndUserNameAsync
            (It.IsAny<Name>() , It.IsAny<UserName>(), It.IsAny<CancellationToken>()))
                .Returns(_moqData.Get());
            GetByNameBankAndUserNameQueryHandler userAndNumberOfShare = new GetByNameBankAndUserNameQueryHandler(
                 _repositorMoq.Object
                , _loggerMoq.Object);


            var getByNameBankAndUserNameQuery = new GetByNameBankAndUserNameQuery()
            {
                NameBankSafe = nameBankSafe,
                UserName = userName
            };
            var result = await userAndNumberOfShare.Handle(getByNameBankAndUserNameQuery, It.IsAny<CancellationToken>());


            Assert.IsType<OperationResult<UserAndNumberOfShare>>(result);
            if (result.IsSuccess)
            {
                Assert.Null(result.Message);

            }
            else
            {
                Assert.NotNull(result.Message);
                Assert.Null(result.Data);
            }

        }
    }

}

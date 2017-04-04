using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ComPact;
using ComPact.Data;
using ComPact.Helpers;
using Newtonsoft.Json;

namespace ComPact
{
	public class MemberDataService: IMemberDataService
	{
		private readonly IMemberWebService _memberWebservice;
		private readonly IMemberRepository _memberRepository;

		public MemberDataService(IMemberWebService memberWebservice, IMemberRepository memberRepository)
		{
			_memberWebservice = memberWebservice;
			_memberRepository = memberRepository;
		}

		/**
		 * Create User
		 */
		public async Task<Member> Create(Member user)
		{
			return await _memberWebservice.Create(APICalls.CreateAuthPath, user);
		}
		public async Task<Member> Get(string email)
		{
			string getUserPathEmailPassword = "/api/users?email=\"" + email + "\"";
			try
			{
				Member member = await _memberWebservice.Read(getUserPathEmailPassword);
				return member;
			}
			catch (Exception)
			{
				throw new Exception("Can't connect to server. Please check your network.");
			}
		}

		public async Task<IEnumerable<Member>> Save(IEnumerable<Member> members)
		{
			try
			{
				await _memberRepository.Insert(members);

			}
			catch (Exception ex)
			{
				Debug.WriteLine(ex);
			}
			return await GetAll();
		}
		public async Task<IEnumerable<Member>> GetAll()
		{
			return await _memberRepository.All();
		}

		public void Forgot(Member user)
		{
			_memberWebservice.Forgot("/api/users/forgot", user);
		}

	}
}
	
		
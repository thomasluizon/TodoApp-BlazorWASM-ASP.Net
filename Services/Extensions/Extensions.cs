namespace Services.Extensions
{
	public static class Extensions
	{
		public static Guid? GetUserId(this HttpContext? httpContext)
		{
			if (httpContext == null) throw new ArgumentNullException(nameof(httpContext));

			try
			{
				return new Guid(httpContext.User.Claims.Single(x => x.Type == "id").Value);
			}
			catch (Exception ex)
			{
				return null;
			}
		}
	}
}

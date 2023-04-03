﻿namespace Undefended.Exceptions;

public class NotFoundException : Exception {
	public NotFoundException(string message) : base(message) { }
}

public class UserNotLoggedInException : Exception {
	public UserNotLoggedInException(string message) : base(message) { }
}

public class ForbiddenException : Exception {
	public ForbiddenException(string message) : base(message) { }
}

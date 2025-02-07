namespace ResultSharp.Errors.Enums
{
    /// <summary>  
    /// Defines error codes for various application and HTTP errors.  
    /// </summary>  
    public enum ErrorCodes
    {
        //=============================  
        //      APPLICATION ERRORS  
        //=============================  
        /// <summary>  
        /// Represents a general failure.  
        /// </summary>  
        Failure, // Not specific  

        /// <summary>  
        /// Represents a creation error.  
        /// </summary>  
        Creation,

        /// <summary>  
        /// Represents a validation error.  
        /// </summary>  
        Validation,

        //=============================  
        //   HTTP ERROR STATUS CODES  
        //=============================  
        // 3xx: Redirection - Further action must be taken in order to complete the request  
        /// <summary>  
        /// Multiple choices available.  
        /// </summary>  
        MultipleChoices = 300,

        /// <summary>  
        /// Resource has been moved permanently.  
        /// </summary>  
        MovedPermanently = 301,

        /// <summary>  
        /// Resource found at a different location.  
        /// </summary>  
        Found = 302,

        /// <summary>  
        /// See other resource.  
        /// </summary>  
        SeeOther = 303,

        /// <summary>  
        /// Resource not modified.  
        /// </summary>  
        NotModified = 304,

        /// <summary>  
        /// Use proxy to access the resource.  
        /// </summary>  
        UseProxy = 305,

        /// <summary>  
        /// Resource temporarily redirected.  
        /// </summary>  
        TemporaryRedirect = 307,

        /// <summary>  
        /// Resource permanently redirected.  
        /// </summary>  
        PermanentRedirect = 308,

        // 4xx: Client Error - The request contains bad syntax or cannot be fulfilled  
        /// <summary>  
        /// Bad request syntax or cannot be fulfilled.  
        /// </summary>  
        BadRequest = 400,

        /// <summary>  
        /// Unauthorized access.  
        /// </summary>  
        Unauthorized = 401,

        /// <summary>  
        /// Payment required to access the resource.  
        /// </summary>  
        PaymentRequired = 402,

        /// <summary>  
        /// Access to the resource is forbidden.  
        /// </summary>  
        Forbidden = 403,

        /// <summary>  
        /// Resource not found.  
        /// </summary>  
        NotFound = 404,

        /// <summary>  
        /// Method not allowed for the resource.  
        /// </summary>  
        MethodNotAllowed = 405,

        /// <summary>  
        /// Resource not acceptable.  
        /// </summary>  
        NotAcceptable = 406,

        /// <summary>  
        /// Proxy authentication required.  
        /// </summary>  
        ProxyAuthenticationRequired = 407,

        /// <summary>  
        /// Request timed out.  
        /// </summary>  
        RequestTimeout = 408,

        /// <summary>  
        /// Conflict with the current state of the resource.  
        /// </summary>  
        Conflict = 409,

        /// <summary>  
        /// Resource is gone.  
        /// </summary>  
        Gone = 410,

        /// <summary>  
        /// Length of the request is required.  
        /// </summary>  
        LengthRequired = 411,

        /// <summary>  
        /// Precondition failed.  
        /// </summary>  
        PreconditionFailed = 412,

        /// <summary>  
        /// Payload too large.  
        /// </summary>  
        PayloadTooLarge = 413,

        /// <summary>  
        /// URI too long.  
        /// </summary>  
        URITooLong = 414,

        /// <summary>  
        /// Unsupported media type.  
        /// </summary>  
        UnsupportedMediaType = 415,

        /// <summary>  
        /// Range not satisfiable.  
        /// </summary>  
        RangeNotSatisfiable = 416,

        /// <summary>  
        /// Expectation failed.  
        /// </summary>  
        ExpectationFailed = 417,

        /// <summary>  
        /// I'm a teapot.  
        /// </summary>  
        ImATeapot = 418,

        /// <summary>  
        /// Misdirected request.  
        /// </summary>  
        MisdirectedRequest = 421,

        /// <summary>  
        /// Unprocessable entity.  
        /// </summary>  
        UnprocessableEntity = 422,

        /// <summary>  
        /// Resource is locked.  
        /// </summary>  
        Locked = 423,

        /// <summary>  
        /// Failed dependency.  
        /// </summary>  
        FailedDependency = 424,

        /// <summary>  
        /// Request is too early.  
        /// </summary>  
        TooEarly = 425,

        /// <summary>  
        /// Upgrade required.  
        /// </summary>  
        UpgradeRequired = 426,

        /// <summary>  
        /// Precondition required.  
        /// </summary>  
        PreconditionRequired = 428,

        /// <summary>  
        /// Too many requests.  
        /// </summary>  
        TooManyRequests = 429,

        /// <summary>  
        /// Request header fields too large.  
        /// </summary>  
        RequestHeaderFieldsTooLarge = 431,

        /// <summary>  
        /// Unavailable for legal reasons.  
        /// </summary>  
        UnavailableForLegalReasons = 451,

        // 5xx: Server Error - The server failed to fulfill an apparently valid request  
        /// <summary>  
        /// Internal server error.  
        /// </summary>  
        InternalServerError = 500,

        /// <summary>  
        /// Not implemented.  
        /// </summary>  
        NotImplemented = 501,

        /// <summary>  
        /// Bad gateway.  
        /// </summary>  
        BadGateway = 502,

        /// <summary>  
        /// Service unavailable.  
        /// </summary>  
        ServiceUnavailable = 503,

        /// <summary>  
        /// Gateway timeout.  
        /// </summary>  
        GatewayTimeout = 504,

        /// <summary>  
        /// HTTP version not supported.  
        /// </summary>  
        HTTPVersionNotSupported = 505,

        /// <summary>  
        /// Variant also negotiates.  
        /// </summary>  
        VariantAlsoNegotiates = 506,

        /// <summary>  
        /// Insufficient storage.  
        /// </summary>  
        InsufficientStorage = 507,

        /// <summary>  
        /// Loop detected.  
        /// </summary>  
        LoopDetected = 508,

        /// <summary>  
        /// Not extended.  
        /// </summary>  
        NotExtended = 510,

        /// <summary>  
        /// Network authentication required.  
        /// </summary>  
        NetworkAuthenticationRequired = 511
    }
}

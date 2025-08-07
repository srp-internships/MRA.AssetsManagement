# Custom Authentication System

This document describes the custom authentication system implemented for the Assets Management Tool.

## Overview

The application now uses a custom authentication system instead of relying on an external identity server. This includes:

- Custom login and registration endpoints
- JWT token-based authentication
- Password hashing using BCrypt
- MongoDB-based user storage

## Features

### Authentication Endpoints

- `POST /api/auth/login` - User login
- `POST /api/auth/register` - User registration

### Default Admin User

When the application starts for the first time, a default admin user is created:

- **Username**: `admin`
- **Email**: `admin@example.com`
- **Password**: `admin123`

**Important**: Change the default password after first login!

## API Usage

### Login

```http
POST /api/auth/login
Content-Type: application/json

{
  "username": "admin",
  "password": "admin123"
}
```

Response:
```json
{
  "accessToken": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "refreshToken": "refresh_token_here",
  "expiresAt": "2024-01-01T12:00:00Z",
  "username": "admin",
  "email": "admin@example.com",
  "roles": ["Administrator"]
}
```

### Register

```http
POST /api/auth/register
Content-Type: application/json

{
  "username": "newuser",
  "email": "user@example.com",
  "password": "password123",
  "confirmPassword": "password123",
  "firstName": "John",
  "lastName": "Doe",
  "phoneNumber": "+1234567890"
}
```

## Configuration

The JWT configuration is in `appsettings.json`:

```json
{
  "JWT": {
    "ExpireDayFromNow": "1",
    "Secret": "your-secret-key-here"
  }
}
```

## Security Features

- Passwords are hashed using BCrypt with salt rounds of 12
- JWT tokens include user ID, username, email, and roles
- Tokens are validated for issuer, audience, and expiration
- Refresh tokens are generated for token renewal

## Frontend Integration

The Blazor client includes:

- Login page at `/login`
- Registration page at `/register`
- Automatic token storage in localStorage
- Automatic token inclusion in HTTP requests
- Logout functionality

## Database Schema

Users are stored in the `employees` collection with the following structure:

```json
{
  "_id": "user_id",
  "firstName": "John",
  "lastName": "Doe",
  "userName": "johndoe",
  "email": "john@example.com",
  "password": "hashed_password",
  "phoneNumber": "+1234567890"
}
```

## Migration from External Identity Server

To migrate from the external identity server:

1. Remove the `IdentityServer` configuration from `appsettings.json`
2. Remove the `Application:Id` and `Application:ClientSecret` configurations
3. Update any hardcoded references to the external identity server
4. Ensure all users are migrated to the new system

## Troubleshooting

### Common Issues

1. **Token validation fails**: Check that the JWT secret is consistent across server restarts
2. **Password not working**: Ensure passwords are properly hashed using BCrypt
3. **User not found**: Check that the user exists in the `employees` collection

### Logs

Check the application logs for authentication-related errors. The system logs:
- Login attempts
- Registration attempts
- Token validation failures
- Database seeding operations 
import { defineStore } from 'pinia'
import { ref, computed } from 'vue'

export interface User {
  id: number
  email: string
  createdAt: string
}

export const useAuthStore = defineStore('auth', () => {
  const token = ref<string | null>(localStorage.getItem('token'))
  const user = ref<User | null>(null)

  const isAuthenticated = computed(() => !!token.value)

  const setToken = (newToken: string) => {
    token.value = newToken
    localStorage.setItem('token', newToken)
  }

  const setUser = (newUser: User) => {
    user.value = newUser
  }

  const logout = () => {
    token.value = null
    user.value = null
    localStorage.removeItem('token')
  }

  const login = async (email: string, password: string) => {
    const response = await fetch('/api/auth/login', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({ email, password })
    })

    if (!response.ok) {
      throw new Error('Login failed')
    }

    const data = await response.json()
    setToken(data.token)
    setUser(data.user)
  }

  const register = async (email: string, password: string) => {
    const response = await fetch('/api/auth/register', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({ email, password })
    })

    if (!response.ok) {
      throw new Error('Registration failed')
    }

    const data = await response.json()
    setToken(data.token)
    setUser(data.user)
  }

  // Load user from token if available
  const loadUser = async () => {
    if (token.value) {
      // Optionally fetch user info, but for now assume it's in token
      // Since JWT has user id, but to get full user, perhaps decode or fetch
      // For simplicity, assume user is set
    }
  }

  return {
    token,
    user,
    isAuthenticated,
    login,
    register,
    logout,
    loadUser
  }
})
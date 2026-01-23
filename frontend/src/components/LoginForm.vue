<template>
  <div class="min-h-screen flex items-center justify-center bg-linear-to-br from-indigo-500 to-purple-600 px-4">
    <div class="w-full max-w-md bg-white rounded-2xl shadow-xl p-8">
      
      <!-- Header -->
      <div class="text-center mb-8">
        <div class="mx-auto mb-4 w-14 h-14 flex items-center justify-center rounded-full bg-indigo-600">
          <svg class="w-7 h-7 text-white" fill="currentColor" viewBox="0 0 24 24">
            <path d="M12 2a10 10 0 100 20 10 10 0 000-20z" />
          </svg>
        </div>
        <h1 class="text-2xl font-semibold text-gray-800">Welcome Back</h1>
        <p class="text-sm text-gray-500">Sign in to your account</p>
      </div>

      <!-- Form -->
      <form @submit.prevent="handleLogin" class="space-y-5">

        <!-- Email -->
        <div>
          <label class="block text-sm font-medium text-gray-700 mb-1">Email</label>
          <input
            type="email"
            v-model="email"
            required
            placeholder="you@example.com"
            class="w-full px-4 py-3 border rounded-lg focus:ring-2 focus:ring-indigo-500 outline-none"
          />
        </div>

        <!-- Password -->
        <div>
          <label class="block text-sm font-medium text-gray-700 mb-1">Password</label>
          <div class="relative">
            <input
              :type="showPassword ? 'text' : 'password'"
              v-model="password"
              required
              placeholder="••••••••"
              class="w-full px-4 py-3 border rounded-lg focus:ring-2 focus:ring-indigo-500 outline-none"
            />
            <button
              type="button"
              @click="showPassword = !showPassword"
              class="absolute right-3 top-1/2 -translate-y-1/2 text-gray-500"
            >
              {{ showPassword ? 'Hide' : 'Show' }}
            </button>
          </div>
        </div>

        <!-- Options -->
        <div class="flex items-center justify-between text-sm">
          <label class="flex items-center gap-2">
            <input type="checkbox" v-model="rememberMe" class="rounded border-gray-300" />
            Remember me
          </label>
          <a href="#" class="text-indigo-600 hover:underline">Forgot password?</a>
        </div>

        <!-- Error -->
        <p v-if="error" class="text-sm text-red-600 bg-red-50 p-3 rounded-lg">
          {{ error }}
        </p>

        <!-- Submit -->
        <button
          type="submit"
          :disabled="loading"
          class="w-full py-3 bg-indigo-600 hover:bg-indigo-700 text-white rounded-lg font-medium transition"
        >
          <span v-if="!loading">Sign In</span>
          <span v-else>Loading...</span>
        </button>

        <!-- Divider -->
        <div class="flex items-center gap-3 text-gray-400 text-sm">
          <div class="flex-1 h-px bg-gray-200"></div>
          or
          <div class="flex-1 h-px bg-gray-200"></div>
        </div>

        <!-- Social -->
        <button class="w-full py-3 border rounded-lg flex items-center justify-center gap-2 hover:bg-gray-50">
          Continue with Google
        </button>

        <!-- Signup -->
        <p class="text-center text-sm text-gray-600">
          Don’t have an account?
          <router-link
            to="/register"
            class="text-indigo-600 font-medium hover:underline"
          >
            Sign up
          </router-link>
        </p>

      </form>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useAuthStore } from '@/stores/authStore'
import { useRouter } from 'vue-router'

const authStore = useAuthStore()
const router = useRouter()

const email = ref('')
const password = ref('')
const loading = ref(false)
const error = ref('')
const showPassword = ref(false)
const rememberMe = ref(false)

const handleLogin = async () => {
  loading.value = true
  error.value = ''
  try {
    await authStore.login(email.value, password.value)
    router.push('/')
  } catch (err: any) {
    error.value = err.message || 'Login failed'
  } finally {
    loading.value = false
  }
}
</script>

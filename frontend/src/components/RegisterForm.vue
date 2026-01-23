<template>
  <div class="min-h-screen bg-linear-to-r from-emerald-50 to-cyan-100 flex items-center justify-center p-4">
    <div class="bg-white rounded-2xl shadow-2xl w-full max-w-md overflow-hidden">
      <!-- Header Section -->
      <div class="bg-linear-to-r from-emerald-500 to-teal-600 p-8 text-white">
        <div class="flex items-center justify-center mb-4">
          <div class="bg-white/20 p-3 rounded-full">
            <svg class="w-8 h-8" fill="currentColor" viewBox="0 0 24 24">
              <path d="M12 12c2.21 0 4-1.79 4-4s-1.79-4-4-4-4 1.79-4 4 1.79 4 4 4zm0 2c-2.67 0-8 1.34-8 4v2h16v-2c0-2.66-5.33-4-8-4z"/>
            </svg>
          </div>
        </div>
        <h1 class="text-2xl font-bold text-center mb-2">Create Your Account</h1>
        <p class="text-center text-emerald-100 text-sm">Join our community and start your journey</p>
      </div>

      <!-- Form Section -->
      <form @submit.prevent="handleRegister" class="p-6 space-y-4">
        <!-- Name Field -->
        

        <!-- Email Field -->
        <div class="space-y-2">
          <label for="email" class="block text-sm font-medium text-gray-700">Email Address</label>
          <div class="relative">
            <div class="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
              <svg class="h-5 w-5 text-gray-400" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M3 8l7.89 4.26a2 2 0 002.22 0L21 8M5 19h14a2 2 0 002-2V7a2 2 0 00-2-2H5a2 2 0 00-2 2v10a2 2 0 002 2z"/>
              </svg>
            </div>
            <input
              id="email"
              v-model="email"
              type="email"
              required
              class="pl-10 w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-teal-500 focus:border-teal-500 transition-all duration-200"
              placeholder="you@example.com"
            />
          </div>
        </div>

        <!-- Password Field -->
        <div class="space-y-2">
          <label for="password" class="block text-sm font-medium text-gray-700">Password</label>
          <div class="relative">
            <div class="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
              <svg class="h-5 w-5 text-gray-400" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 15v2m-6 4h12a2 2 0 002-2v-6a2 2 0 00-2-2H6a2 2 0 00-2 2v6a2 2 0 002 2zm10-10V7a4 4 0 00-8 0v4h8z"/>
              </svg>
            </div>
            <input
              :type="showPassword ? 'text' : 'password'"
              id="password"
              v-model="password"
              required
              class="pl-10 pr-10 w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-teal-500 focus:border-teal-500 transition-all duration-200"
              placeholder="••••••••"
            />
            <button
              type="button"
              @click="showPassword = !showPassword"
              class="absolute inset-y-0 right-0 pr-3 flex items-center"
            >
              <svg class="h-5 w-5 text-gray-400 hover:text-gray-600" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path v-if="showPassword" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M15 12a3 3 0 11-6 0 3 3 0 016 0z"/>
                <path v-if="showPassword" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M2.458 12C3.732 7.943 7.523 5 12 5c4.478 0 8.268 2.943 9.542 7-1.274 4.057-5.064 7-9.542 7-4.477 0-8.268-2.943-9.542-7z"/>
                <path v-if="!showPassword" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M13.875 18.825A10.05 10.05 0 0112 19c-4.478 0-8.268-2.943-9.543-7a9.97 9.97 0 011.563-3.029m5.858.908a3 3 0 114.243 4.243M9.878 9.878l4.242 4.242M9.878 9.878L6.59 6.59m7.532 7.532l3.29 3.29M3 3l3.59 3.59m0 0A9.953 9.953 0 0112 5c4.478 0 8.268 2.943 9.543 7a10.025 10.025 0 01-4.132 5.411m0 0L21 21"/>
              </svg>
            </button>
          </div>
          <!-- Password Strength Meter -->
          
        </div>

        <!-- Confirm Password Field -->
        <div class="space-y-2">
          <label for="confirmPassword" class="block text-sm font-medium text-gray-700">Confirm Password</label>
          <div class="relative">
            <div class="absolute inset-y-0 left-0 pl-3 flex items-center pointer-events-none">
              <svg class="h-5 w-5 text-gray-400" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 12l2 2 4-4m6 2a9 9 0 11-18 0 9 9 0 0118 0z"/>
              </svg>
            </div>
            <input
              :type="showConfirmPassword ? 'text' : 'password'"
              id="confirmPassword"
              v-model="confirmPassword"
              required
              class="pl-10 pr-10 w-full px-3 py-2 border border-gray-300 rounded-lg focus:ring-2 focus:ring-teal-500 focus:border-teal-500 transition-all duration-200"
              placeholder="••••••••"
            />
            <button
              type="button"
              @click="showConfirmPassword = !showConfirmPassword"
              class="absolute inset-y-0 right-0 pr-3 flex items-center"
            >
              <svg class="h-5 w-5 text-gray-400 hover:text-gray-600" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path v-if="showConfirmPassword" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M15 12a3 3 0 11-6 0 3 3 0 016 0z"/>
                <path v-if="showConfirmPassword" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M2.458 12C3.732 7.943 7.523 5 12 5c4.478 0 8.268 2.943 9.542 7-1.274 4.057-5.064 7-9.542 7-4.477 0-8.268-2.943-9.542-7z"/>
                <path v-if="!showConfirmPassword" stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M13.875 18.825A10.05 10.05 0 0112 19c-4.478 0-8.268-2.943-9.543-7a9.97 9.97 0 011.563-3.029m5.858.908a3 3 0 114.243 4.243M9.878 9.878l4.242 4.242M9.878 9.878L6.59 6.59m7.532 7.532l3.29 3.29M3 3l3.59 3.59m0 0A9.953 9.953 0 0112 5c4.478 0 8.268 2.943 9.543 7a10.025 10.025 0 01-4.132 5.411m0 0L21 21"/>
              </svg>
            </button>
          </div>
          <div v-if="!passwordsMatch && confirmPassword" class="text-sm text-red-600 flex items-center gap-1">
            <svg class="w-4 h-4" fill="currentColor" viewBox="0 0 20 20">
              <path fill-rule="evenodd" d="M10 18a8 8 0 100-16 8 8 0 000 16zM8.707 7.293a1 1 0 00-1.414 1.414L8.586 10l-1.293 1.293a1 1 0 101.414 1.414L10 11.414l1.293 1.293a1 1 0 001.414-1.414L11.414 10l1.293-1.293a1 1 0 00-1.414-1.414L10 8.586 8.707 7.293z" clip-rule="evenodd"/>
            </svg>
            Passwords do not match
          </div>
        </div>

        <!-- Terms and Conditions -->
        <div class="flex items-start space-x-2">
          <input
            id="terms"
            v-model="acceptTerms"
            type="checkbox"
            class="mt-1 h-4 w-4 text-teal-600 focus:ring-teal-500 border-gray-300 rounded"
          />
          <label for="terms" class="text-sm text-gray-600">
            I agree to the 
            <a href="#" class="text-teal-600 hover:text-teal-700 font-medium">Terms of Service</a>
            and 
            <a href="#" class="text-teal-600 hover:text-teal-700 font-medium">Privacy Policy</a>
          </label>
        </div>

        <!-- Error Message -->
        <div v-if="error" class="bg-red-50 border-l-4 border-red-500 p-4 rounded">
          <div class="flex items-center">
            <svg class="h-5 w-5 text-red-500 mr-2" fill="currentColor" viewBox="0 0 20 20">
              <path fill-rule="evenodd" d="M10 18a8 8 0 100-16 8 8 0 000 16zM8.707 7.293a1 1 0 00-1.414 1.414L8.586 10l-1.293 1.293a1 1 0 101.414 1.414L10 11.414l1.293 1.293a1 1 0 001.414-1.414L11.414 10l1.293-1.293a1 1 0 00-1.414-1.414L10 8.586 8.707 7.293z" clip-rule="evenodd"/>
            </svg>
            <span class="text-red-700 text-sm">{{ error }}</span>
          </div>
        </div>

        <!-- Submit Button -->
        <button
          type="submit"
          :disabled="loading || !acceptTerms || !passwordsMatch"
          :class="{
            'opacity-50 cursor-not-allowed': loading || !acceptTerms || !passwordsMatch,
            'hover:shadow-lg transform hover:-translate-y-0.5': !loading && acceptTerms && passwordsMatch
          }"
          class="w-full bg-linear-to-r from-emerald-500 to-teal-600 text-white font-semibold py-3 px-4 rounded-lg shadow-md transition-all duration-200 flex items-center justify-center"
        >
          <svg v-if="loading" class="animate-spin -ml-1 mr-3 h-5 w-5 text-white" fill="none" viewBox="0 0 24 24">
            <circle class="opacity-25" cx="12" cy="12" r="10" stroke="currentColor" stroke-width="4"/>
            <path class="opacity-75" fill="currentColor" d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z"/>
          </svg>
          <span>{{ loading ? 'Creating Account...' : 'Create Account' }}</span>
        </button>
        <!-- Sign In Link -->
        <div class="text-center pt-4 border-t border-gray-200">
          <p class="text-gray-600 text-sm">
            Already have an account?
            <router-link to="/login" class="text-teal-600 hover:text-teal-700 font-semibold ml-1">
              Sign in here
            </router-link>
          </p>
        </div>
      </form>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, watch } from 'vue'
import { useAuthStore } from '@/stores/authStore'
import { useRouter } from 'vue-router'

const authStore = useAuthStore()
const router = useRouter()

const email = ref('')
const password = ref('')
const confirmPassword = ref('')
const loading = ref(false)
const error = ref('')
const showPassword = ref(false)
const showConfirmPassword = ref(false)
const acceptTerms = ref(false)

// Password strength calculation
const strength = computed(() => {
  let score = 0
  if (password.value.length >= 8) score += 1
  if (/[A-Z]/.test(password.value)) score += 1
  if (/[a-z]/.test(password.value)) score += 1
  if (/[0-9]/.test(password.value)) score += 1
  if (/[^A-Za-z0-9]/.test(password.value)) score += 1
  return score
})

const strengthPercentage = computed(() => {
  return (strength.value / 5) * 100
})

const strengthText = computed(() => {
  if (password.value.length === 0) return 'Enter password'
  if (strength.value <= 1) return 'Weak'
  if (strength.value <= 3) return 'Medium'
  return 'Strong'
})

const strengthColorClass = computed(() => {
  if (strength.value <= 1) return 'text-red-500'
  if (strength.value <= 3) return 'text-yellow-500'
  return 'text-emerald-500'
})

const strengthBarClass = computed(() => {
  if (strength.value <= 1) return 'bg-red-500'
  if (strength.value <= 3) return 'bg-yellow-500'
  return 'bg-emerald-500'
})

const passwordsMatch = computed(() => {
  if (!password.value || !confirmPassword.value) return true
  return password.value === confirmPassword.value
})

const handleRegister = async () => {
  loading.value = true
  error.value = ''
  
  if (!passwordsMatch.value) {
    error.value = 'Passwords do not match'
    loading.value = false
    return
  }
  
  try {
    await authStore.register(email.value, password.value)
    router.push('/')
  } catch (err: any) {
    error.value = err.message || 'Registration failed. Please try again.'
  } finally {
    loading.value = false
  }
}
</script>


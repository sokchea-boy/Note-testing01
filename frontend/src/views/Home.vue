<template>
  <div class="container mx-auto px-4 py-8">
    <div class="flex justify-between items-center mb-8">
      <h1 class="text-3xl font-bold text-gray-900">Notes</h1>
      <div class="flex items-center space-x-4">
        <span v-if="authStore.user">Welcome, {{ authStore.user.email }}</span>
        <router-link
          to="/note/new"
          class="inline-flex items-center px-4 py-2 border border-transparent text-sm font-medium rounded-md text-white bg-indigo-600 hover:bg-indigo-700"
        >
          Create Note
        </router-link>
        <button
          @click="handleLogout"
          class="inline-flex items-center px-4 py-2 border border-transparent text-sm font-medium rounded-md text-white bg-gray-600 hover:bg-gray-700"
        >
          Logout
        </button>
      </div>
    </div>
    <div v-if="notesStore.loading" class="text-center">Loading...</div>
    <div v-else-if="notesStore.error" class="text-center text-red-600">{{ notesStore.error }}</div>
    <NoteList v-else />
  </div>
</template>

<script setup lang="ts">
import { onMounted } from 'vue'
import NoteList from '@/components/NoteList.vue'
import { useNotesStore } from '@/stores/notesStore'
import { useAuthStore } from '@/stores/authStore'
import { useRouter } from 'vue-router'

const notesStore = useNotesStore()
const authStore = useAuthStore()
const router = useRouter()

const handleLogout = () => {
  authStore.logout()
  router.push('/login')
}

onMounted(() => {
  notesStore.fetchNotes()
})
</script>
<template>
  <div class="max-w-2xl mx-auto px-4 py-8">
    <button
      @click="router.push('/')"
      class="mb-6 flex items-center text-indigo-600 hover:text-indigo-800 transition-colors duration-200 group"
    >
      <svg 
        class="w-5 h-5 mr-2 transform group-hover:-translate-x-1 transition-transform duration-200" 
        fill="none" 
        stroke="currentColor" 
        viewBox="0 0 24 24"
      >
        <path 
          stroke-linecap="round" 
          stroke-linejoin="round" 
          stroke-width="2" 
          d="M10 19l-7-7m0 0l7-7m-7 7h18"
        />
      </svg>
      Back
    </button>

    <div class="bg-white shadow-xl rounded-xl p-8 transition-all duration-300 hover:shadow-2xl">
      <div class="border-l-4 border-indigo-500 pl-4 mb-6">
        <h1 class="text-3xl font-bold text-gray-900 tracking-tight">{{ note.title }}</h1>
      </div>
      
      <div class="mb-8">
        <p class="text-gray-700 leading-relaxed whitespace-pre-wrap text-lg">
          {{ note.content }}
        </p>
      </div>

      <div class="bg-gray-50 rounded-lg p-4 mb-8">
        <div class="flex flex-wrap gap-4 text-sm text-gray-600">
          <div class="flex items-center">
            <svg class="w-4 h-4 mr-2 text-gray-400" fill="none" stroke="currentColor" viewBox="0 0 24 24">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M8 7V3m8 4V3m-9 8h10M5 21h14a2 2 0 002-2V7a2 2 0 00-2-2H5a2 2 0 00-2 2v12a2 2 0 002 2z" />
            </svg>
            <span class="font-medium">Created:</span>
            <span class="ml-1">{{ formatDate(note.createdAt) }}</span>
          </div>
          <div class="flex items-center">
            <svg class="w-4 h-4 mr-2 text-gray-400" fill="none" stroke="currentColor" viewBox="0 0 24 24">
              <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M12 8v4l3 3m6-3a9 9 0 11-18 0 9 9 0 0118 0z" />
            </svg>
            <span class="font-medium">Updated:</span>
            <span class="ml-1">{{ formatDate(note.updatedAt) }}</span>
          </div>
        </div>
      </div>

      <div class="flex space-x-4 pt-6 border-t border-gray-100">
        <router-link
          :to="`/note/${note.id}/edit`"
          class="inline-flex items-center px-5 py-2.5 border border-transparent text-sm font-medium rounded-lg text-white bg-linear-to-br from-indigo-600 to-indigo-700 hover:from-indigo-700 hover:to-indigo-800 shadow-md hover:shadow-lg transition-all duration-200 transform hover:-translate-y-0.5"
        >
          <svg class="w-4 h-4 mr-2" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M11 5H6a2 2 0 00-2 2v11a2 2 0 002 2h11a2 2 0 002-2v-5m-1.414-9.414a2 2 0 112.828 2.828L11.828 15H9v-2.828l8.586-8.586z" />
          </svg>
          Edit 
        </router-link>
        <button
          @click="handleDelete"
          class="inline-flex items-center px-5 py-2.5 border border-transparent text-sm font-medium rounded-lg text-white bg-linear-to-r from-red-500 to-red-600 hover:from-red-600 hover:to-red-700 shadow-md hover:shadow-lg transition-all duration-200 transform hover:-translate-y-0.5"
        >
          <svg class="w-4 h-4 mr-2" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M19 7l-.867 12.142A2 2 0 0116.138 21H7.862a2 2 0 01-1.995-1.858L5 7m5 4v6m4-6v6m1-10V4a1 1 0 00-1-1h-4a1 1 0 00-1 1v3M4 7h16" />
          </svg>
          Delete
        </button>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { useRouter } from 'vue-router'
import type { Note } from '@/types'
import { useNotesStore } from '@/stores/notesStore'

const props = defineProps<{
  note: Note
}>()

const router = useRouter()
const notesStore = useNotesStore()

function goBack() {
  router.go(-1)
}

async function handleDelete() {
  if (confirm('Are you sure you want to delete this note?')) {
    try {
      await notesStore.deleteNote(props.note.id)
      router.push('/')
    } catch (error) {
      alert('Failed to delete note')
    }
  }
}

function formatDate(dateString: string): string {
  return new Date(dateString).toLocaleString()
}
</script>

<style scoped>
/* Custom scrollbar for note content */
.whitespace-pre-wrap {
  max-height: 60vh;
  overflow-y: auto;
}

.whitespace-pre-wrap::-webkit-scrollbar {
  width: 6px;
}

.whitespace-pre-wrap::-webkit-scrollbar-track {
  background: #f1f1f1;
  border-radius: 3px;
}

.whitespace-pre-wrap::-webkit-scrollbar-thumb {
  background: #c7d2fe;
  border-radius: 3px;
}

.whitespace-pre-wrap::-webkit-scrollbar-thumb:hover {
  background: #a5b4fc;
}
</style>
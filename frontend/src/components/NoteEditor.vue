<template>
  <div class="min-h-screen bg-linear-to-br from-gray-50 to-indigo-50 p-4 md:p-6">
    <div class="max-w-4xl mx-auto">
      <!-- Header -->
      <div class="mb-8">
        <div class="flex items-center justify-between">
          <div>
            <h1 class="text-2xl md:text-3xl font-bold text-gray-900">
              {{ isEdit ? 'Edit Note' : 'Create New Note' }}
            </h1>
            <p class="text-gray-600 mt-2">
              {{ isEdit ? 'Update your thoughts and ideas' : 'Capture your thoughts and ideas' }}
            </p>
          </div>
          <div class="flex items-center space-x-3">
            <button
              @click="router.push('/')"
              class="inline-flex items-center px-4 py-2 border border-gray-300 rounded-lg text-gray-700 bg-white hover:bg-gray-50 transition-all duration-200 shadow-sm hover:shadow"
            >
              <svg class="w-5 h-5 mr-2" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M10 19l-7-7m0 0l7-7m-7 7h18"/>
              </svg>
              Back
            </button>
          </div>
        </div>
      </div>

      <!-- Form Card -->
      <div class="bg-white rounded-2xl shadow-xl overflow-hidden">
        <form @submit.prevent="handleSave" class="p-6 md:p-8 space-y-6">
          <!-- Title Section -->
          <div class="space-y-2">
            <div class="flex items-center justify-between">
              <label for="title" class="block text-sm font-semibold text-gray-700">
                Title
                <span class="text-red-500 ml-1">*</span>
              </label>

            </div>
            <div class="relative">
              <div class="absolute left-3 top-1/2 transform -translate-y-1/2">
                <svg class="w-5 h-5 text-gray-400" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M11 5H6a2 2 0 00-2 2v11a2 2 0 002 2h11a2 2 0 002-2v-5m-1.414-9.414a2 2 0 112.828 2.828L11.828 15H9v-2.828l8.586-8.586z"/>
                </svg>
              </div>
              <input
                id="title"
                v-model="title"
                type="text"
                maxlength="100"
                placeholder="Enter a descriptive title"
                class="pl-10 w-full px-4 py-3 border-2 border-gray-200 rounded-xl focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500 transition-all duration-200 text-lg font-medium"
                required
              />
            </div>
         
          </div>

          <!-- Content Section -->
          <div class="space-y-2">
            <div class="flex items-center justify-between">
              <label for="content" class="block text-sm font-semibold text-gray-700">
                Content
                <span class="text-red-500 ml-1">*</span>
              </label>
            </div>
            <div class="relative">
              <div class="absolute left-3 top-4">
                <svg class="w-5 h-5 text-gray-400" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M9 12h6m-6 4h6m2 5H7a2 2 0 01-2-2V5a2 2 0 012-2h5.586a1 1 0 01.707.293l5.414 5.414a1 1 0 01.293.707V19a2 2 0 01-2 2z"/>
                </svg>
              </div>
              <textarea
                id="content"
                v-model="content"
                rows="12"
                maxlength="5000"
                placeholder="Write your note here... Markdown supported!"
                class="pl-10 w-full px-4 py-3 border-2 border-gray-200 rounded-xl focus:ring-2 focus:ring-indigo-500 focus:border-indigo-500 transition-all duration-200 resize-none font-mono text-gray-800"
                required
              ></textarea>
            </div>
            <!-- Markdown Preview Toggle -->
          
          </div>

         

          <!-- Footer Actions -->
          <div class="flex flex-col sm:flex-row items-center justify-between pt-6 border-t border-gray-200">
            <div class="flex items-center space-x-4 mb-4 sm:mb-0">
              <div class="flex items-center">
                <input
                  id="pinned"
                  v-model="isPinned"
                  type="checkbox"
                  class="h-4 w-4 text-indigo-600 focus:ring-indigo-500 border-gray-300 rounded"
                />
                <label for="pinned" class="ml-2 text-sm text-gray-600">
                  Pin this note
                </label>
              </div>
              <div class="flex items-center">
                <input
                  id="favorite"
                  v-model="isFavorite"
                  type="checkbox"
                  class="h-4 w-4 text-yellow-500 focus:ring-yellow-500 border-gray-300 rounded"
                />
                <label for="favorite" class="ml-2 text-sm text-gray-600">
                  Favorite
                </label>
              </div>
            </div>
            <div class="flex items-center space-x-4">
              <button
                type="button"
                @click="handleCancel"
                class="px-6 py-2.5 border-2 border-gray-300 text-gray-700 font-medium rounded-xl hover:bg-gray-50 transition-all duration-200"
              >
                Cancel
              </button>
              <button
                type="submit"
                :disabled="loading"
                :class="{
                  'opacity-75 cursor-not-allowed': loading,
                  'hover:shadow-lg transform hover:-translate-y-0.5': !loading
                }"
                class="inline-flex items-center px-6 py-2.5 bg-linear-to-r from-indigo-600 to-purple-600 text-white font-semibold rounded-xl shadow-md transition-all duration-200"
              >
                <svg v-if="loading" class="animate-spin -ml-1 mr-3 h-5 w-5 text-white" fill="none" viewBox="0 0 24 24">
                  <circle class="opacity-25" cx="12" cy="12" r="10" stroke="currentColor" stroke-width="4"/>
                  <path class="opacity-75" fill="currentColor" d="M4 12a8 8 0 018-8V0C5.373 0 0 5.373 0 12h4zm2 5.291A7.962 7.962 0 014 12H0c0 3.042 1.135 5.824 3 7.938l3-2.647z"/>
                </svg>
                <svg v-else class="w-5 h-5 mr-2" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path stroke-linecap="round" stroke-linejoin="round" stroke-width="2" d="M8 7H5a2 2 0 00-2 2v9a2 2 0 002 2h14a2 2 0 002-2V9a2 2 0 00-2-2h-3m-1 4l-3 3m0 0l-3-3m3 3V4"/>
                </svg>
                {{ loading ? 'Saving...' : (isEdit ? 'Update Note' : 'Save Note') }}
              </button>
            </div>
          </div>
        </form>
      </div>

     
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted, watch } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { useNotesStore } from '@/stores/notesStore'
import { getNoteByIdApi } from '@/composables/useNotes'


const route = useRoute()
const router = useRouter()
const notesStore = useNotesStore()

const title = ref('')
const content = ref('')
const isEdit = ref(false)
const noteId = ref<string | null>(null)
const loading = ref(false)
const tags = ref<string[]>([])
const tagInput = ref('')
const isPinned = ref(false)
const isFavorite = ref(false)



// Watch for changes
watch(content, (newContent) => {
  if (newContent.length > 5000) {
    content.value = newContent.slice(0, 5000)
  }
})

watch(title, (newTitle) => {
  if (newTitle.length > 100) {
    title.value = newTitle.slice(0, 100)
  }
})

onMounted(async () => {
  const id = route.params.id as string
  if (id && route.path.includes('/edit')) {
    isEdit.value = true
    noteId.value = id
    try {
      const note = await getNoteByIdApi(id)
      title.value = note.title
      content.value = note.content
     
    } catch (error) {
      console.error('Failed to fetch note for editing:', error)
      alert('Failed to load note for editing')
    }
  }
})

// Tag handling functions
function addTag() {
  const tag = tagInput.value.trim()
  if (tag && !tags.value.includes(tag)) {
    tags.value.push(tag)
    tagInput.value = ''
  }
}

function removeTag(index: number) {
  tags.value.splice(index, 1)
}

function handleBackspace() {
  if (!tagInput.value && tags.value.length > 0) {
    tags.value.pop()
  }
}

// Text formatting helper
function formatText(type: 'bold' | 'italic') {
  const textarea = document.getElementById('content') as HTMLTextAreaElement
  const start = textarea.selectionStart
  const end = textarea.selectionEnd
  const selectedText = content.value.substring(start, end)
  
  let formattedText = ''
  switch (type) {
    case 'bold':
      formattedText = `**${selectedText}**`
      break
    case 'italic':
      formattedText = `*${selectedText}*`
      break
  }
  
  content.value = 
    content.value.substring(0, start) + 
    formattedText + 
    content.value.substring(end)
    
  textarea.focus()
  textarea.setSelectionRange(start + formattedText.length, start + formattedText.length)
}

async function handleSave() {
  loading.value = true
  try {
    const noteData = {
      title: title.value.trim(),
      content: content.value.trim(),
      tags: tags.value,
      isPinned: isPinned.value,
      isFavorite: isFavorite.value
    }

    if (isEdit.value && noteId.value) {
      await notesStore.updateNote(noteId.value, noteData)
      router.push(`/note/${noteId.value}`)
    } else {
      const newNote = await notesStore.createNote(noteData)
      router.push(`/note/${newNote.id}`)
    }
  } catch (error) {
    alert('Failed to save note. Please try again.')
    console.error('Save error:', error)
  } finally {
    loading.value = false
  }
}

function handleCancel() {
  if (isEdit.value && noteId.value) {
    router.push(`/note/${noteId.value}`)
  } else {
    router.push('/')
  }
}
</script>

<style scoped>
/* Custom scrollbar for textarea */
textarea::-webkit-scrollbar {
  width: 8px;
}

textarea::-webkit-scrollbar-track {
  background: #f1f1f1;
  border-radius: 4px;
}

textarea::-webkit-scrollbar-thumb {
  background: #c1c1c1;
  border-radius: 4px;
}

textarea::-webkit-scrollbar-thumb:hover {
  background: #a1a1a1;
}

/* Smooth transitions */
input, textarea, button {
  transition: all 0.2s ease-in-out;
}

/* Focus styles */
input:focus, textarea:focus {
  box-shadow: 0 0 0 3px rgba(99, 102, 241, 0.1);
}
</style>